using HN_Backend.Data;
using HN_Backend.Interface;
using HN_Backend.Repository;
using HN_Project.DTOs;
using HN_Project.Interface;
using HN_Shared.DTOs;

namespace HN_Project.Service
{
    public class ProductService
    {
        private readonly IProductRepositroy _productRepo;
        public ProductService(IProductRepositroy productRepo)
        {
            _productRepo = productRepo;
        } 
        public async Task<List<ProductVM>> GetAllProductList()
        {
            return await _productRepo.GetAllProductList();
        }
        public async Task<List<BrandVM>> GetAllProductBrandList()
        {
            return await _productRepo.GetAllProductBrandList();
        }
        public async Task<List<CategoryVM>> GetAllProductCategoryList()
        {
            return await _productRepo.GetAllProductCategoryList();
        }
        public async Task<List<ProductGroupVM>> GetAllProductGroupList()
        {
            return await _productRepo.GetAllProductGroupList();
        }
        public async Task <ProductVM> GetProductByName( string name)
        {
            return await _productRepo.GetProductByName(name);
        }
        public async Task<ProductVM> GetProductById(long Id)
        {
            return await _productRepo.GetProductById(Id);
        }
        //public async Task SaveProduct(ProductVM productVM)
        //{
        //    await _productRepo.SaveProduct(productVM);
        //}

        public async Task SaveProduct(ProductVM vm)
        {
            var product = new Product
            {
                Name = vm.Name,
                Code = vm.Code,
                Model = vm.Model,
                SerialAvailable = vm.SerialAvailable,
                Price = vm.Price,
                Discount = vm.Discount,
                Vat = vm.Vat,
                Tax = vm.Tax,
                Warranty = vm.Warranty,
                Picture = vm.Picture,
                EntryBy = 1,
                GroupId = vm.GroupId,
                CategoryId = vm.CategoryId,
                BrandId = vm.BrandId
            };

            await _productRepo.SaveProduct(product);
        }
    }
}
