using HN_Project.DTOs;
using HN_Project.Interface;
using HN_Project.Models;

namespace HN_Project.Service
{
    public class SearchingByDapperService
    {
        private readonly ISearchingByDapperRepository _searchDapp;
        public SearchingByDapperService(ISearchingByDapperRepository searchDapp)
        {
            _searchDapp = searchDapp;
        }
        public async Task<List<CurrentStockProductVM>> GetProductByNameCodeSerialModelNoWithCurrentStock(string objParam)
        {
            return await _searchDapp.GetProductByNameCodeSerialModelNoWithCurrentStock(objParam);
        }
    }
}
