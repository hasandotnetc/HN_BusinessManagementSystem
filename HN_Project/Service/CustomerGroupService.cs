using HN_Backend.DTOs;
using HN_Backend.DTOs.CustomerGroup;
using HN_Backend.Enums;
using HN_Backend.Helpers;
using HN_Backend.Interface;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace HN_Backend.Service
{
    public class CustomerGroupService
    {
        private readonly ICustomerGroup _customerGroupRepo;
        private readonly CurrentSessionData _currentSessionData;
        private readonly IEventNoOrCodeGeneration _eventNoOrCodeGeneration;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerGroupService(ICustomerGroup customerGroupRepo,CurrentSessionData currentSessionData, IEventNoOrCodeGeneration eventNoOrCodeGeneration, IUnitOfWork unitOfWork)
        {
            _customerGroupRepo = customerGroupRepo;
            _currentSessionData = currentSessionData;
            _eventNoOrCodeGeneration = eventNoOrCodeGeneration;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CustomerGroupDropdownDto>> GetCustomerGroupDropdownAsync()
        {  
            long companyId = _currentSessionData.CompanyId;
            return await _customerGroupRepo.GetCustomerGroupDropdownAsync(companyId);
        }

        public async Task<ServiceResult<string>> CreateCustomerGroupAsync(CustomerGroupCreateDto customerGroupDto)
        {
            try
            {
                long companyId = _currentSessionData.CompanyId;
                long userId = _currentSessionData.UserId;

                await _unitOfWork.BeginTransactionAsync();
                var code = await _eventNoOrCodeGeneration.EventCodeGeneration(EventTypeEnum.CustomerGroup.ToString(), "CSG", companyId);

                customerGroupDto.CompanyId = companyId;
                customerGroupDto.EntryBy = userId;
                customerGroupDto.Code = code;
                await _customerGroupRepo.CreateCustomerGroupAsync(customerGroupDto);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return await Task.FromResult(new ServiceResult<string>
                {
                    Success = true,
                    Message = "Customer group created successfully.",
                    Data = code
                });
            }
            catch (Exception Ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return await Task.FromResult(new ServiceResult<string>
                {
                    Success = false,
                    Message = "Customer group created successfully.", 
                });
            }
        }

    }
}
