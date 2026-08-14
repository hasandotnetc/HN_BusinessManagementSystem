using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Interface;
using HN_Backend.Repository;
using HN_Project.DTOs;
using HN_Project.Interface;
//using HN_Shared.DTOs;

namespace HN_Project.Service
{
    public class ProductService
    {
        private readonly IProductRepositroy _productRepo;
        private readonly ImageService _imageService;
        public ProductService(IProductRepositroy productRepo, ImageService imgServ)
        {
            _productRepo = productRepo;
            _imageService = imgServ;
        } 
        public async Task<List<ProductVM>> GetAllProductList()
        {
            return await _productRepo.GetAllProductList();
        }
        public async Task<List<BrandVM>> GetAllProductBrandList()
        {
            return await _productRepo.GetAllProductBrandList();
        }
        public async Task<List<ProductCategoryVM>> GetAllProductCategoryList()
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


        public async Task SaveProduct(ProductVM vm)
        {
            string? imagePath = null;

            try
            {
                if (vm.ProductImage != null)
                {
                    imagePath = await _imageService.SaveImageAsync(vm.ProductImage, "Product");
                }

                var product = new Product
                {
                    Name = vm.Name,
                    Code = vm.Code??"",
                    Model = vm.Model,
                    SerialAvailable = vm.SerialAvailable,
                    Price = vm.Price,
                    Discount = vm.Discount,
                    Vat = vm.Vat,
                    Tax = vm.Tax,
                    Warranty = vm.Warranty,
                    Picture = imagePath,
                    EntryBy = 1,
                    GroupId = vm.GroupId,
                    CategoryId = vm.CategoryId,
                    BrandId = vm.BrandId
                };

                await _productRepo.SaveProduct(product);
            }
            catch
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    await _imageService.DeleteImageAsync(imagePath);
                }

                throw;
            }
        }

        public async Task SaveProductBrand(BrandVM brand)
        {
            string? imagePath = null;

            try
            {
                if (brand.BrandImage != null)
                {
                    imagePath = await _imageService.SaveImageAsync(brand.BrandImage, "Brand");
                }

                var _brand = new Brand
                {
                    Name = brand.Name,
                    Code = brand.Code??"123", 
                    Picture = imagePath,
                    EntryBy = 1, 
                };

                await _productRepo.SaveProductBrand(_brand);
            }
            catch
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    await _imageService.DeleteImageAsync(imagePath);
                } 
                throw;
            }
        }

        public async Task SaveProductCategory(ProductCategoryVM pcVM)
        {
            string? imagePath = null;

            try
            {
                if (pcVM.CategoryImage != null)
                {
                    imagePath = await _imageService.SaveImageAsync(pcVM.CategoryImage, "Category");
                }

                var _category = new Category
                {
                    Name = pcVM.Name,
                    Code = pcVM.Code??"123",
                    Picture = imagePath,
                    EntryBy = 1,
                };

                await _productRepo.SaveProductCategory(_category);
            }
            catch
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    await _imageService.DeleteImageAsync(imagePath);
                }
                throw;
            }
        }

        public async Task SaveProductGroup(ProductGroupVM pgVM)
        {
            var _productGroup = new ProductGroup
            {
                Name = pgVM.Name,
                EntryBy = 1,
            };

            await _productRepo.SaveProductGroup(_productGroup);
        }

    }
}
