using HN_Backend.Data;
using HN_Backend.DTOs.ProductBrand;
using HN_Backend.DTOs.ProductCategory;
using HN_Backend.DTOs.ProductGroup;
using HN_Backend.DTOs.Products;
using HN_Backend.DTOs.UnitType;
using HN_Backend.Interface;
//using HN_Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProductSearchAutocompleteDto>> GetProductSearchAutocompleteList(string nameOrCodeOrModelParam, long GroupId, long BrandId, long CategoryId, long companyId)
        {
            return await _db.Products.Where(p => p.CompanyId == companyId 
            && p.ActiveStatus == "Y"
            && (p.Name.Contains(nameOrCodeOrModelParam) || p.Code.Contains(nameOrCodeOrModelParam) || p.Model.Contains(nameOrCodeOrModelParam))
            && (GroupId == 0 || p.GroupId == GroupId) && (BrandId == 0 || p.BrandId == BrandId) && (CategoryId == 0 || p.CategoryId == CategoryId)
            ).AsNoTracking().Select(b => new ProductSearchAutocompleteDto
            {
                ProductId = b.ProductId,
                ProductName = b.Name 
            }).Take(10).ToListAsync();
        }

        public async Task<List<ProductVM>> GetAllProductList()
        {
            return await _db.Products.AsNoTracking().Select(b => new ProductVM
            {
                ProductId = b.ProductId,
                Name = b.Name
            }).ToListAsync();
        }

        public async Task<List<BrandVM>> GetAllProductBrandList(long companyId)
        {
            return await _db.Brands.Where(x => x.CompanyId == companyId).AsNoTracking().Select(b => new BrandVM
            {
                BrandId = b.BrandId,
                Name = b.Name
            }).ToListAsync();

        }

        public async Task<List<ProductCategoryVM>> GetAllProductCategoryList(long companyId)
        {
            return await _db.Categories.Where(x=>x.CompanyId == companyId).AsNoTracking().Select(b => new ProductCategoryVM
            {
                CategoryId = b.CategoryId,
                Name = b.Name
            }).ToListAsync();
        }

        public async Task<List<UnitType>> GetAllUnitTypeList()
        {
            return await _db.UnitTypes.AsNoTracking().Select(b => new UnitType
            {
                UnitTypeId = b.UnitTypeId,
                Name = b.Name
            }).ToListAsync();
        }


        public async Task<List<ProductGroupVM>> GetAllProductGroupList(long companyId)
        {
            return await _db.ProductGroups.Where(x=>x.CompanyId==companyId).AsNoTracking().Select(b => new ProductGroupVM
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



        public async Task<ProductVM> GetProductByIdAndUnitTypeId(long id,long unitTypeId, long companyId)
        {
            return await _db.Products.Where(p => p.ProductId == id && p.UnitTypeId == unitTypeId && p.CompanyId == companyId).Select(b => new ProductVM
            {
                ProductId = b.ProductId,
                Name = b.Name
            }).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductAllInformationById(long id)
        {
            return await _db.Products.Where(p => p.ProductId == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductAllInformationByCode(string code)
        {
            return await _db.Products.Where(p => p.Code == code).AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<ProductVM> GetProductByName(string name)
        {
            return await _db.Products.Where(p => p.Name == name).AsNoTracking().Select(b => new ProductVM
            {
                ProductId = b.ProductId,
                Name = b.Name
            }).FirstOrDefaultAsync();
        }



        public async Task<ProductGroup> GetProductGroupByName(string name,long companyId)
        {
            return await _db.ProductGroups.Where(p => p.Name == name && p.CompanyId == companyId).AsNoTracking().Select(b => new ProductGroup
            {
                ProductGroupId = b.ProductGroupId,
                Name = b.Name
            }).FirstOrDefaultAsync();
        }

        public async Task<Brand> GetBrandByName(string name, long companyId)
        {
            return await _db.Brands.Where(p => p.Name == name && p.CompanyId == companyId).AsNoTracking().Select(b => new Brand
            {
                BrandId = b.BrandId,
                Name = b.Name
            }).FirstOrDefaultAsync();
        }
        public async Task<Category> GetCategoryByName(string name, long companyId)
        {
            return await _db.Categories.Where(p => p.Name == name && p.CompanyId == companyId).AsNoTracking().Select(b => new Category
            {
                CategoryId = b.CategoryId,
                Name = b.Name
            }).FirstOrDefaultAsync();
        }

        public async Task SaveProductUnitTypeConversion(ProductUnitTypeConversion _productConver)
        {
            await _db.ProductUnitTypeConversions.AddAsync(_productConver);
        }
        public async Task<ProductBaseUnitTypeAndConversionRatioDto> GetProductBaseUnitTypeAndConversionRatio(long ProductId, long CompanyId)
        {
            return await (from p in _db.Products
                                       join u in _db.UnitTypes on p.UnitTypeId equals u.UnitTypeId
                                       join conv in _db.ProductUnitTypeConversions on new { p.ProductId, p.UnitTypeId, p.CompanyId } equals new { conv.ProductId, conv.UnitTypeId, conv.CompanyId } 
                                       where p.ProductId == ProductId && p.CompanyId == CompanyId
                                       select new ProductBaseUnitTypeAndConversionRatioDto
                                       {
                                           BaseUnitTypeId = u.UnitTypeId,
                                           BaseUnitTypeName = u.Name,
                                           BaseUnitConversionRatio = conv.ConversionToUnitType
                                       }).FirstOrDefaultAsync();
             
        }


        public async Task<List<ProductAllUnitTypePurchaseAllowDto>> GetProductUnitTypeForPurchase(long ProductId, long CompanyId)
        {
            var _list = await (from p in _db.Products
                               join putc in _db.ProductUnitTypeConversions on p.ProductId equals putc.ProductId
                               join ut in _db.UnitTypes on putc.UnitTypeId equals ut.UnitTypeId
                               where putc.ProductId == ProductId && p.CompanyId == CompanyId
                               select new ProductAllUnitTypePurchaseAllowDto
                               {
                                   Name = ut.Name,
                                   UnitTypeId = ut.UnitTypeId,
                                   DefaultPurchase = putc.IsDefaultPurchase
                               }).ToListAsync();
            return _list;

        }

        public async Task SaveProduct(Product _product)
        {
            await _db.Products.AddAsync(_product); 
        }
        public async Task SaveProductBrand(Brand _brand)
        {
            await _db.Brands.AddAsync(_brand); 
        }
        public async Task SaveProductCategory(Category _category)
        {
            await _db.Categories.AddAsync(_category); 
        }
        public async Task SaveProductGroup(ProductGroup _productGroup)
        {
            await _db.ProductGroups.AddAsync(_productGroup); 
        } 
        public async Task UpdateProduct(Product _product)
        {
            _db.Products.Update(_product);
        }
        public async Task UpdateProductUnitTypeConversion(ProductUnitTypeConversion _productConver)
        {
             _db.ProductUnitTypeConversions.Update(_productConver);
        }

        public async Task<ProductUnitTypeConversion> GetProductUnitTypeConversion(long productId, long unitTypeId, long companyId)
        {
            return await _db.ProductUnitTypeConversions.Where(p => p.ProductId == productId && p.UnitTypeId == unitTypeId && p.CompanyId == companyId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ProductUnitTypeConversion> GetProductUnitTypeConversionDefaultCheck(long productId, long companyId)
        {
            return await _db.ProductUnitTypeConversions.Where(p => p.ProductId == productId && p.CompanyId == companyId && (p.IsDefaultSale == true || p.IsDefaultPurchase == true) ).AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<bool> HasOtherDefaultPurchaseUnitType(long productId, long unitTypeId, long companyId)
        {
            return await _db.ProductUnitTypeConversions
                .AnyAsync(x =>
                    x.ProductId == productId &&
                    x.UnitTypeId != unitTypeId &&
                    x.CompanyId == companyId &&
                    x.IsDefaultPurchase == true);
        }


        public async Task<bool> HasOtherDefaultSaleUnitType(long productId, long unitTypeId, long companyId)
        {
            return await _db.ProductUnitTypeConversions
                .AnyAsync(x =>
                    x.ProductId == productId &&
                    x.UnitTypeId != unitTypeId &&
                    x.CompanyId == companyId &&
                    x.IsDefaultSale == true);
        }

    }
}
