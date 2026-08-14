using HN_Backend.DTOs;
using HN_Project.DTOs;

namespace HN_Project.Interface
{
    public interface ISearchingByDapperRepository
    {
        Task<List<CurrentStockProductVM>> GetProductByNameCodeSerialModelNoWithCurrentStock(string objParam);
        Task<List<CurrentStockProductVM>> GetProductByNameCodeModelNoWithCurrentStock(string objParam);
        Task<PaginationResponse<ProductPaginationVM>> GetProductByPaginationRequest(PaginationRequest request);
    } 
}
