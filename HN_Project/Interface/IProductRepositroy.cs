
using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Project.DTOs;
//using HN_Shared.DTOs;

namespace HN_Backend.Interface
{
    public interface IProductRepositroy 
    {
        Task<List<ProductVM>> GetAllProductList();
        Task<List<ProductGroupVM>> GetAllProductGroupList();
        Task<List<BrandVM>> GetAllProductBrandList();
        Task<List<ProductCategoryVM>> GetAllProductCategoryList();
        Task<ProductVM> GetProductById(long id);
        Task<ProductVM> GetProductByName(string name);
        Task SaveProduct(Product product);
        Task SaveProductBrand(Brand brand);
        Task SaveProductCategory(Category category);
        Task SaveProductGroup(ProductGroup productGroup);
    }
}
