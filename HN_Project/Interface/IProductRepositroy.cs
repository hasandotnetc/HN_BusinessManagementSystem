
using HN_Backend.Data;
using HN_Backend.DTOs.ProductBrand;
using HN_Backend.DTOs.ProductCategory;
using HN_Backend.DTOs.ProductGroup;
using HN_Backend.DTOs.Products;
using HN_Backend.DTOs.UnitType;

//using HN_Shared.DTOs;

namespace HN_Backend.Interface
{
    public interface IProductRepository 
    {
        Task<List<ProductSearchAutocompleteDto>> GetProductSearchAutocompleteList(string nameOrCodeOrModelParam, long GroupId, long BrandId, long CategoryId, long companyId);
        Task<List<ProductVM>> GetAllProductList();
        Task<List<ProductGroupVM>> GetAllProductGroupList(long companyId);
        Task<List<BrandVM>> GetAllProductBrandList(long companyId);
        Task<List<ProductCategoryVM>> GetAllProductCategoryList(long companyId);
        Task<List<UnitType>> GetAllUnitTypeList();
        Task<ProductVM> GetProductById(long id);
        Task<ProductVM> GetProductByIdAndUnitTypeId(long id, long unitTypeId, long companyId);
        Task<Product> GetProductAllInformationById(long id);
        Task<Product> GetProductAllInformationByCode(string code);
        Task<ProductVM> GetProductByName(string name);
        Task<ProductGroup> GetProductGroupByName(string name,long companyId);
        Task<Brand> GetBrandByName(string name,long companyId);
        Task<Category> GetCategoryByName(string name, long companyId);
        Task<ProductBaseUnitTypeAndConversionRatioDto> GetProductBaseUnitTypeAndConversionRatio(long productId, long companyId);
        Task SaveProductUnitTypeConversion(ProductUnitTypeConversion prCon); 
        Task SaveProduct(Product product);
        Task SaveProductBrand(Brand brand);
        Task SaveProductCategory(Category category);
        Task SaveProductGroup(ProductGroup productGroup);
        Task UpdateProduct(Product product);
        Task UpdateProductUnitTypeConversion(ProductUnitTypeConversion product);
        Task<ProductUnitTypeConversion> GetProductUnitTypeConversion(long productId, long unitTypeId, long companyId);
        Task<ProductUnitTypeConversion> GetProductUnitTypeConversionDefaultCheck(long productId,  long companyId);
        public Task<bool> HasOtherDefaultPurchaseUnitType(long productId, long unitTypeId, long companyId);
        public Task<bool> HasOtherDefaultSaleUnitType(long productId, long unitTypeId, long companyId);
        Task<List<ProductAllUnitTypePurchaseAllowDto>> GetProductUnitTypeForPurchase(long ProductId, long CompanyId);
    }
}
