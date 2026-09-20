using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.DTOs.ProductBrand;
using HN_Backend.DTOs.ProductCategory;
using HN_Backend.DTOs.ProductGroup;
using HN_Backend.DTOs.Products;
using HN_Backend.DTOs.UnitType;
using HN_Backend.Helpers;
using HN_Backend.Interface;
//using HN_Backend.Models;
using HN_Backend.Repository;
using HN_Project.Interface;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
//using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
//using HN_Shared.DTOs;

namespace HN_Project.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ImageService _imageService;
        private readonly CurrentSessionData _currentSessionData;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventNoOrCodeGeneration _eventNoOrCodeGeneration;
        public ProductService(IProductRepository productRepo, ImageService imgServ, CurrentSessionData currentSessionData,IUnitOfWork unitOfWork,IEventNoOrCodeGeneration eventNoOrCodeGeneration)
        {
            _productRepo = productRepo;
            _imageService = imgServ;
            _currentSessionData = currentSessionData;
            _unitOfWork = unitOfWork;
            _eventNoOrCodeGeneration = eventNoOrCodeGeneration;
        } 
        public async Task<List<ProductSearchAutocompleteDto>> GetProductSearchAutocompleteList(string nameOrCodeOrModelParam, long GroupId, long BrandId, long CategoryId)
        {
            return await _productRepo.GetProductSearchAutocompleteList(nameOrCodeOrModelParam, GroupId, BrandId, CategoryId, _currentSessionData.CompanyId);
        }
        public async Task<List<ProductVM>> GetAllProductList()
        {
            return await _productRepo.GetAllProductList();
        }
        public async Task<List<BrandVM>> GetAllProductBrandList()
        {
            long userId = _currentSessionData.UserId;
            long companyId = _currentSessionData.CompanyId;
            return await _productRepo.GetAllProductBrandList(companyId);
        }
        public async Task<List<ProductCategoryVM>> GetAllProductCategoryList()
        {
            long userId = _currentSessionData.UserId;
            long companyId = _currentSessionData.CompanyId;
            return await _productRepo.GetAllProductCategoryList(companyId);
        }

        public async Task<List<UnitType>> GetAllUnitTypeList()
        {
            return await _productRepo.GetAllUnitTypeList();
        }

        public async Task<List<ProductGroupVM>> GetAllProductGroupList()
        {
            long userId = _currentSessionData.UserId;
            long companyId = _currentSessionData.CompanyId;
            return await _productRepo.GetAllProductGroupList(companyId);
        }
        public async Task <ProductVM> GetProductByName( string name)
        {
            return await _productRepo.GetProductByName(name);
        }
        public async Task<ProductVM> GetProductById(long Id)
        {
            return await _productRepo.GetProductById(Id);
        }
        public async Task<ProductBaseUnitTypeAndConversionRatioDto> GetProductBaseUnitTypeAndConversionRatio(long ProductId)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _productRepo.GetProductBaseUnitTypeAndConversionRatio(ProductId, companyId);
        }

        public async Task<List<ProductAllUnitTypePurchaseAllowDto>> GetProductUnitTypeForPurchase(long ProductId)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _productRepo.GetProductUnitTypeForPurchase(ProductId, companyId);
        }
         

        public async Task<ServiceResult<string>> SaveProduct(ProductVM vm)
        {
            long userId = _currentSessionData.UserId;
            long companyId = _currentSessionData.CompanyId;
            long locationId = _currentSessionData.LocationId;
            string? imagePath = null;

            try
            {
                if (vm.ProductImage != null)
                {
                    imagePath = await _imageService.SaveImageAsync(vm.ProductImage, "Product");
                }
                await _unitOfWork.BeginTransactionAsync();
                var code = await _eventNoOrCodeGeneration.EventCodeGeneration("Product", "PR", companyId);
                var product = new Product
                {
                    Name = vm.Name,
                    Code = code,
                    Model = vm.Model,
                    SerialAvailable = vm.SerialAvailable, 
                    ActiveStatus = vm.IsActive,
                    Vat = vm.VAT, 
                    IsVatPercentageOrAmount = vm.VatMode, 
                    Warranty = vm.Warranty,
                    Picture = imagePath,
                    EntryBy = userId,
                    CompanyId = companyId,
                    GroupId = vm.GroupId,
                    CategoryId = vm.CategoryId,
                    BrandId = vm.BrandId,
                    ProductType = (vm.ProductType== "Inventory")?"I":"S",
                    ProductNote = vm.Note,
                    UnitTypeId = vm.UnitTypeId
                };
                var productUnitTypeConversion = new ProductUnitTypeConversion
                {
                    ProductId = product.ProductId,
                    UnitTypeId = vm.UnitTypeId,
                    ConversionToUnitType = 1, 
                    EntryDate = DateTime.Now,
                    EntryBy = userId,
                    CompanyId = companyId,
                    IsPurchaseAllowed = true,
                    IsSaleAllowed = true,
                    IsActive = true,
                    IsDefaultPurchase = true,
                    IsDefaultSale = true,
                    Product = product 
                }; 

                await _productRepo.SaveProduct(product);
                await _productRepo.SaveProductUnitTypeConversion(productUnitTypeConversion);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Product saved successfully.",
                    Data = code
                };
            }
            catch(Exception ex)
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

        public async Task<ServiceResult<long>> SaveProductBrand(BrandVM brand)
        {
            try
            {
                long userId = _currentSessionData.UserId;
                long companyId = _currentSessionData.CompanyId;
                long locationId = _currentSessionData.LocationId;
                var _brand = new Brand
                {
                    Name = brand.Name,
                    EntryBy = userId,
                    CompanyId = companyId
                };
                var productBrandAlreadyExistingCheck = await _productRepo.GetBrandByName(brand.Name, companyId);
                if (productBrandAlreadyExistingCheck != null)
                {
                    return new ServiceResult<long>
                    {
                        Success = false,
                        Message = "Duplicate Brand name found."
                    };
                }


                await _productRepo.SaveProductBrand(_brand);
                await _unitOfWork.SaveChangesAsync();
                long BrandId = _brand.BrandId;

                return new ServiceResult<long>
                {
                    Success = true,
                    Message = "Product Brand saved successfully.",
                    Data = BrandId
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<long>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<long>> SaveProductCategory(ProductCategoryVM pcVM)
        {
            try
            {
                long userId = _currentSessionData.UserId;
                long companyId = _currentSessionData.CompanyId;
                var _category = new Category
                {
                    Name = pcVM.Name,
                    EntryBy = userId,
                    CompanyId = companyId
                };

                var productCategoryAlreadyExistingCheck = await _productRepo.GetCategoryByName(pcVM.Name, companyId);
                if (productCategoryAlreadyExistingCheck != null)
                {
                    return new ServiceResult<long>
                    {
                        Success = false,
                        Message = "Duplicate Category name found."
                    };
                }

                await _productRepo.SaveProductCategory(_category);
                await _unitOfWork.SaveChangesAsync();
                long CategoryId = _category.CategoryId;

                return new ServiceResult<long>
                {
                    Success = true,
                    Message = "Product Category saved successfully.",
                    Data = CategoryId
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<long>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ServiceResult<long>> SaveProductGroup(ProductGroupVM pgVM)
        {
            try
            {
                long companyId = _currentSessionData.CompanyId;
                long userId = _currentSessionData.UserId;
                var _productGroup = new ProductGroup
                {
                    Name = pgVM.Name,
                    EntryBy = userId,
                    CompanyId = companyId
                };
                var productGroupAlreadyExistingCheck = await _productRepo.GetProductGroupByName(pgVM.Name, companyId);
                if (productGroupAlreadyExistingCheck != null)
                {
                    return new ServiceResult<long>
                    {
                        Success = false,
                        Message = "Duplicate Product group name found."
                    };
                }
                await _productRepo.SaveProductGroup(_productGroup);
                await _unitOfWork.SaveChangesAsync();

                long productGroupId = _productGroup.ProductGroupId;

                return new ServiceResult<long>
                {
                    Success = true,
                    Message = "Product Group saved successfully.",
                    Data = productGroupId
                };
            }
            catch (Exception ex)
            { 
                return new ServiceResult<long>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }


        public async Task<ServiceResult<string>> UpdateProduct(ProductVM vm)
        {
            long userId = _currentSessionData.UserId;
            long companyId = _currentSessionData.CompanyId;

            string? newImagePath = null;
            string? oldImagePath = null;

            try
            {
                // 1. Get existing product
                // var product = await _productRepo.GetProductAllInformationById(vm.ProductId);
                var product = await _productRepo.GetProductAllInformationByCode(vm.Code);

                if (product == null)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Product not found."
                    };
                } 
                oldImagePath = product.Picture; 
                if (vm.ProductImage != null)
                {
                    newImagePath = await _imageService.SaveImageAsync(vm.ProductImage, "Product");
                } 
                await _unitOfWork.BeginTransactionAsync(); 
                product.Name = vm.Name;
                product.Model = vm.Model;
                //product.SerialAvailable = vm.SerialAvailable;
                product.ActiveStatus = vm.IsActive;
                product.Vat = vm.VAT;
                product.IsVatPercentageOrAmount = vm.VatMode;
                product.Warranty = vm.Warranty; 
                if (!string.IsNullOrEmpty(newImagePath))
                {
                    product.Picture = newImagePath;
                } 
                product.GroupId = vm.GroupId;
                product.CategoryId = vm.CategoryId;
                product.BrandId = vm.BrandId;
                product.ProductType = vm.ProductType == "Inventory" ? "I" : "S";

                product.ProductNote = vm.Note;
                //product.UnitTypeId = vm.UnitTypeId; 

                await _productRepo.UpdateProduct(product);
                await _unitOfWork.SaveChangesAsync();
                 
                await _unitOfWork.CommitTransactionAsync();
                 
                if (!string.IsNullOrEmpty(newImagePath) &&
                    !string.IsNullOrEmpty(oldImagePath))
                {
                    await _imageService.DeleteImageAsync(oldImagePath);
                }
                 
                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Product updated successfully.",
                    Data = product.Code
                };
            }
            catch (Exception ex)
            { 
                await _unitOfWork.RollbackTransactionAsync(); 
                if (!string.IsNullOrEmpty(newImagePath))
                {
                    await _imageService.DeleteImageAsync(newImagePath);
                }

                return new ServiceResult<string>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task <ServiceResult<long>> SaveProductUnitTypeConversionAsync(UnitTypeConversionCreateDto unitTypeConversionCreateDto)
        {
            long userId = _currentSessionData.UserId;
            long companyId = _currentSessionData.CompanyId;
            var productUnitTypeConversion = new ProductUnitTypeConversion
            {
                ProductId = unitTypeConversionCreateDto.ProductId,
                UnitTypeId = unitTypeConversionCreateDto.UnitTypeId,
                ConversionToUnitType = unitTypeConversionCreateDto.ConversionToUnitType,
                EntryDate = DateTime.Now,
                EntryBy = userId,
                CompanyId = companyId,
                IsPurchaseAllowed = unitTypeConversionCreateDto.IsPurchaseAllowed,
                IsSaleAllowed = unitTypeConversionCreateDto.IsSaleAllowed,
                IsActive = unitTypeConversionCreateDto.IsActive,
                IsDefaultPurchase = unitTypeConversionCreateDto.IsDefaultPurchase,
                IsDefaultSale = unitTypeConversionCreateDto.IsDefaultSale, 
            };

            try
            {
                var product = await _productRepo.GetProductUnitTypeConversion(unitTypeConversionCreateDto.ProductId,unitTypeConversionCreateDto.UnitTypeId,companyId);
                if (product !=null)
                {
                    return new ServiceResult<long>
                    {
                        Success = false,
                        Message = "Product and UnitType Conversion already has created !" 
                    };

                }

                await _unitOfWork.BeginTransactionAsync();
                await _productRepo.SaveProductUnitTypeConversion(productUnitTypeConversion);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ServiceResult<long>
                {
                    Success = true,
                    Message = "Conversion saved successfully.",
                    Data = unitTypeConversionCreateDto.ProductId
                };

            }
            catch (SqlException ex )
            {
                await _unitOfWork.CommitTransactionAsync();
                return new ServiceResult<long>
                {
                    Success = false,
                    Message = "Conversion failed!!" 
                };
            }

        }

        public async Task<ServiceResult<long>> UpdateProductUnitTypeConversionAsync(UnitTypeConversionUpdateDto unitTypeConversionUpdateDto)
        {
            long userId = _currentSessionData.UserId;
            long companyId = _currentSessionData.CompanyId;
            var productConversion = await _productRepo.GetProductUnitTypeConversion(unitTypeConversionUpdateDto.ProductId, unitTypeConversionUpdateDto.UnitTypeId, companyId);
            //var defaultPurchaseOrSalesCheck = await _productRepo.GetProductUnitTypeConversionDefaultCheck(unitTypeConversionUpdateDto.ProductId, companyId);

            var hasOtherDefaultPurchase = await _productRepo.HasOtherDefaultPurchaseUnitType(unitTypeConversionUpdateDto.ProductId, unitTypeConversionUpdateDto.UnitTypeId, companyId);
            var hasOtherDefaultSale = await _productRepo.HasOtherDefaultSaleUnitType(unitTypeConversionUpdateDto.ProductId, unitTypeConversionUpdateDto.UnitTypeId, companyId);

            if (unitTypeConversionUpdateDto.IsDefaultPurchase && hasOtherDefaultPurchase)
            {
                return new ServiceResult<long>
                {
                    Success = false,
                    Message = "Sorry, multiple default Unit Type is not allowed for Purchase!"
                };
            }

            if (unitTypeConversionUpdateDto.IsDefaultSale && hasOtherDefaultSale)
            {
                return new ServiceResult<long>
                {
                    Success = false,
                    Message = "Sorry, multiple default Unit Type is not allowed for Sales!"
                };
            }



            //if (defaultPurchaseOrSalesCheck !=null && defaultPurchaseOrSalesCheck.IsDefaultPurchase == true)
            //{
            //    return new ServiceResult<long>
            //    {
            //        Success = false,
            //        Message = "Sorry, multiple default Unit Type is not allowed for Purchase !",
            //    };
            //}
            //if (defaultPurchaseOrSalesCheck != null && defaultPurchaseOrSalesCheck.IsDefaultSale == true)
            //{
            //    return new ServiceResult<long>
            //    {
            //        Success = false,
            //        Message = "Sorry, multiple default Unit Type is not allowed for Sales !",
            //    };
            //}

            if (productConversion != null)
            {
                productConversion.UpdateDate = DateTime.Now;
                productConversion.UpdateBy = userId;
                productConversion.IsPurchaseAllowed = unitTypeConversionUpdateDto.IsPurchaseAllowed;
                productConversion.IsSaleAllowed = unitTypeConversionUpdateDto.IsSaleAllowed;
                productConversion.IsDefaultSale = unitTypeConversionUpdateDto.IsDefaultSale;
                productConversion.IsDefaultPurchase = unitTypeConversionUpdateDto.IsDefaultPurchase;
                productConversion.IsActive = unitTypeConversionUpdateDto.IsActive;
                productConversion.ConversionToUnitType = unitTypeConversionUpdateDto.ConversionToUnitType;
            }
            try
            {  
                await _unitOfWork.BeginTransactionAsync();
                await _productRepo.UpdateProductUnitTypeConversion(productConversion);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ServiceResult<long>
                {
                    Success = true,
                    Message = "Conversion update successfully.", 
                };
            }
            catch (SqlException ex)
            {
                await _unitOfWork.CommitTransactionAsync();
                return new ServiceResult<long>
                {
                    Success = false,
                    Message = "Conversion failed!!"
                };
            }

        }
    }
}
