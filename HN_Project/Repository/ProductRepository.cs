using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Interface;
using HN_Project.DTOs;
//using HN_Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class ProductRepository:IProductRepositroy
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<ProductVM>> GetAllProductList()
        {
            return await _db.Products.AsNoTracking().Select(b => new ProductVM
            {
                ProductId = b.ProductId,
                Name = b.Name
            }).ToListAsync();
        }

        public async Task<List<BrandVM>> GetAllProductBrandList()
        {
            return await _db.Brands.AsNoTracking().Select(b => new BrandVM
            {
                BrandId = b.BrandId,
                Name = b.Name
            }).ToListAsync();

        }

        public async Task<List<ProductCategoryVM>> GetAllProductCategoryList()
        {
            return await _db.Categories.AsNoTracking().Select(b => new ProductCategoryVM
            {
                CategoryId = b.CategoryId,
                Name = b.Name
            }).ToListAsync();
        }

        public async Task<List<ProductGroupVM>> GetAllProductGroupList()
        {
            return await _db.ProductGroups.AsNoTracking().Select(b => new ProductGroupVM
            {
                ProductGroupId = b.ProductGroupId,
                Name = b.Name
            }).ToListAsync();
        }

    
        public async Task<ProductVM> GetProductById(long id)
        {
            return await _db.Products.Where(p => p.ProductId == id).AsNoTracking().Select(b => new ProductVM
            {
                ProductId = b.ProductId,
                Name = b.Name
            }).FirstOrDefaultAsync();
        }

        public async Task<ProductVM> GetProductByName(string name)
        {
            return await _db.Products.Where(p => p.Name == name).AsNoTracking().Select(b => new ProductVM
            {
                ProductId = b.ProductId,
                Name = b.Name
            }).FirstOrDefaultAsync();
        }

        public async Task SaveProduct(Product _product)
        {
            _db.Products.Add(_product);
            await _db.SaveChangesAsync();
        }
        public async Task SaveProductBrand(Brand _brand)
        {
            _db.Brands.Add(_brand);
            await _db.SaveChangesAsync();
        }
        public async Task SaveProductCategory(Category _category)
        {
            _db.Categories.Add(_category);
            await _db.SaveChangesAsync();
        }
        public async Task SaveProductGroup(ProductGroup _productGroup)
        {
            _db.ProductGroups.Add(_productGroup);
            await _db.SaveChangesAsync();
        }


    }
}
