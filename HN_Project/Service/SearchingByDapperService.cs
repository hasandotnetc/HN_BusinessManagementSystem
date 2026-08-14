using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Project.DTOs;
using HN_Project.Interface;

namespace HN_Project.Service
{
    public class SearchingByDapperService
    {
        private readonly ISearchingByDapperRepository _searchDapp;
        public SearchingByDapperService(ISearchingByDapperRepository searchDapp)
        {
            _searchDapp = searchDapp;
        }

        public async Task<List<CurrentStockProductVM>> GetProductByNameCodeModelNoWithCurrentStock(string objParam)
        {
            return await _searchDapp.GetProductByNameCodeModelNoWithCurrentStock(objParam);
        }


        public async Task<List<CurrentStockProductVM>> GetProductByNameCodeSerialModelNoWithCurrentStock(string objParam)
        {
            return await _searchDapp.GetProductByNameCodeSerialModelNoWithCurrentStock(objParam);
        }

        public async Task<PaginationResponse<ProductPaginationVM>> GetProductByPaginationRequest(PaginationRequest request)
        {
            return await _searchDapp.GetProductByPaginationRequest(request);
        }
    }
}
