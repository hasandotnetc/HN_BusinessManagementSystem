using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.DTOs.PaginationDto;
using HN_Backend.DTOs.Products;
using HN_Backend.Helpers;
using HN_Project.DTOs;
using HN_Project.Interface;

namespace HN_Project.Service
{
    public class SearchingByDapperService
    {
        private readonly ISearchingByDapperRepository _searchDapp;
        private readonly CurrentSessionData _currentSessionData;
        public SearchingByDapperService(ISearchingByDapperRepository searchDapp, CurrentSessionData currentSessionData)
        {
            _searchDapp = searchDapp;
            _currentSessionData = currentSessionData;
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
        public async Task<PaginationResponse<CustomerPaginationDto>> GetCustomerByPaginationRequest(PaginationRequest request)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _searchDapp.GetCustomerByPaginationRequest(request, companyId);
        }

        public async Task<PaginationResponse<ProductUnitTypeConversionLoadGridDto>> GetProductUnitTypeConversionRatio(ProductUnitTypeConversionPaginationRequest request)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _searchDapp.GetProductUnitTypeConversionRatio(request, companyId);
        }
        public async Task<List<ProductSearchAutocompleteDetailsDto>> GetProductUnitTypeConversionRatio(string objParam)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _searchDapp.GetProductDetailWithStock(objParam, companyId);
        }
    }
}
