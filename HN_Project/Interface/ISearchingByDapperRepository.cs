using HN_Project.DTOs;

namespace HN_Project.Interface
{
    public interface ISearchingByDapperRepository
    {
        Task<List<CurrentStockProductVM>> GetProductByNameCodeSerialModelNoWithCurrentStock(string objParam);
    }
}
