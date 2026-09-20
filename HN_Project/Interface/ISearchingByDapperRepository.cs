using HN_Backend.DTOs;
using HN_Backend.DTOs.PaginationDto;
using HN_Backend.DTOs.Products;
using HN_Project.DTOs;

namespace HN_Project.Interface
{
    public interface ISearchingByDapperRepository
    {
        Task<List<CurrentStockProductVM>> GetProductByNameCodeSerialModelNoWithCurrentStock(string objParam);
        Task<List<CurrentStockProductVM>> GetProductByNameCodeModelNoWithCurrentStock(string objParam);
        Task<PaginationResponse<ProductPaginationVM>> GetProductByPaginationRequest(PaginationRequest request);
        Task<PaginationResponse<CustomerPaginationDto>> GetCustomerByPaginationRequest(PaginationRequest request,long CompanyId);
        Task<PaginationResponse<ProductUnitTypeConversionLoadGridDto>> GetProductUnitTypeConversionRatio(ProductUnitTypeConversionPaginationRequest request,long CompanyId);
        Task<List<ProductSearchAutocompleteDetailsDto>> GetProductDetailWithStock(string objParam,long CompanyId);
    } 
}
