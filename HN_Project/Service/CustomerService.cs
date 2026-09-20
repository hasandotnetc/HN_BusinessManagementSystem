using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.DTOs.Customer;
using HN_Backend.Helpers;
using HN_Backend.Interface;
using HN_Project.Interface;
using Microsoft.Data.SqlClient;

namespace HN_Project.Service
{
    
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ImageService _imageService;
        private readonly CurrentSessionData _currentSessionData;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventNoOrCodeGeneration _eventNoOrCodeGeneration;
        private readonly ISupplier _supplier;
        public CustomerService(ICustomerRepository customerRepo,ImageService imageService, CurrentSessionData currentSessionData, IUnitOfWork unitOfWork,IEventNoOrCodeGeneration eventNo,ISupplier supplier)
        {
            _customerRepo = customerRepo;
            _imageService = imageService;
            _currentSessionData = currentSessionData;
            _unitOfWork = unitOfWork;
            _eventNoOrCodeGeneration = eventNo;
            _supplier = supplier;
        }
        public async Task<List<Customer>> GetCustomerByCodeNameAndPhone(string objParam) 
        {
            long companyId = _currentSessionData.CompanyId;
            long userId = _currentSessionData.UserId;
            return await _customerRepo.GetCustomerByCodeNameAndPhone(objParam,companyId);
        }
        public async Task<ServiceResult<string>> CreateCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            long companyId = _currentSessionData.CompanyId;
            long userId = _currentSessionData.UserId;
            var imagePath = ""; 
            try
            {
                if (customerCreateDto.CustomerImage != null)
                {
                    imagePath = await _imageService.SaveImageAsync(customerCreateDto.CustomerImage, "Customer");
                }

                await _unitOfWork.BeginTransactionAsync();
                var customerCode = await _eventNoOrCodeGeneration.EventCodeGeneration(HN_Backend.Enums.EventTypeEnum.Customer.ToString(), "CUS", companyId);
                Supplier? supplier = null;
                if (customerCreateDto.CombineWithSupplier == true)
                {
                    var supplierCode = await _eventNoOrCodeGeneration.EventCodeGeneration(HN_Backend.Enums.EventTypeEnum.Supplier.ToString(), "SUP", companyId);
                    supplier = new Supplier
                    {
                        Name = customerCreateDto.Name,
                        Phone = customerCreateDto.Phone,
                        Code = supplierCode,
                        Address = customerCreateDto.Address,
                        Picture = imagePath,
                        Email = customerCreateDto.Email,
                        EntryBy = userId,
                        CompanyId = companyId
                    };
                    await _supplier.CreateSupplierAsync(supplier);
                } 
                var customer = new Customer
                {
                    Code = customerCode,
                    Name = customerCreateDto.Name,
                    Phone = customerCreateDto.Phone,
                    Email = customerCreateDto.Email,
                    Address = customerCreateDto.Address,
                    CompanyId = companyId,
                    EntryBy = userId,
                    Picture = imagePath,
                    ActiveStatus = customerCreateDto.ActiveStatus ?? "Y", 
                    Nid = customerCreateDto.Nid,
                    OpeningBalance = customerCreateDto.OpeningBalance,
                    IsOwnCompanyCustomer = customerCreateDto.IsOwnCompanyCustomer,
                    CustomerGroupId = customerCreateDto.CustomerGroupId,
                    //SupplierId = customerCreateDto.SupplierId,

                    Supplier = supplier
                };
                 

                await _customerRepo.CreateCustomerAsync(customer);
              

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Product saved successfully.",
                    Data = customerCode
                };
            }
            catch (SqlException ex)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    await _imageService.DeleteImageAsync(imagePath);
                }
                await _unitOfWork.RollbackTransactionAsync();
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ServiceResult<string>> UpdateCustomerAsync(CustomerUpdateDto customerUpdateDto)
        {
            long companyId = _currentSessionData.CompanyId;
            long userId = _currentSessionData.UserId;
            var imagePath = "";
            try
            {
                var customerList = await _customerRepo.GetCustomerByCodeNameAndPhone(customerUpdateDto.Code, companyId);
                var customer = customerList.FirstOrDefault();

                if (customerUpdateDto.ProductImage != null && customer !=null)
                {
                    imagePath = await _imageService.SaveImageAsync(customerUpdateDto.ProductImage, "Customer");
                }
                if (customer !=null && customer.SupplierId !=null)
                {
                    var supplier = await _supplier.GetSupplierByIdAsync(customer.SupplierId.Value, companyId);  
                    supplier.Name = customerUpdateDto.Name;
                    supplier.Phone = customerUpdateDto.Phone;
                    supplier.Email = customerUpdateDto.Email;
                    supplier.Address = customerUpdateDto.Address;
                    supplier.UpdateOn = DateTime.Now;
                    supplier.UpdateBy = userId;
                    await _supplier.UpdateSupplierAsync(supplier);
                } 
                await _unitOfWork.BeginTransactionAsync(); 

                customer.Name = customerUpdateDto.Name;
                customer.Phone = customerUpdateDto.Phone;
                customer.Email = customerUpdateDto.Email;
                customer.Address = customerUpdateDto.Address;
                customer.Nid = customerUpdateDto.Nid;
                customer.Picture = imagePath;
                customer.ActiveStatus = customerUpdateDto.ActiveStatus ?? "Y";
                customer.CustomerGroupId = customerUpdateDto.CustomerGroupId;
                customer.IsOwnCompanyCustomer = customerUpdateDto.IsOwnCompanyCustomer;
                customer.UpdateBy = userId;
                customer.UpdateOn = DateTime.Now; 

                await _imageService.DeleteImageAsync(customer.Picture);
                await _customerRepo.UpdateCustomerAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Customer updated successfully."
                };
            }
            catch (SqlException ex)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    await _imageService.DeleteImageAsync(imagePath);
                }
                await _unitOfWork.RollbackTransactionAsync();
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }

    }
}
 