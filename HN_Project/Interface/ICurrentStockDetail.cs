using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface ICurrentStockDetail
    {
        Task<CurrentStockDetail> GetCurrentStocDetailkByIdAsync(long id);
        Task CreateCurrentStockDetailAsync(CurrentStockDetail CurrentStockDetail);
        Task UpdateCurrentStockDetailAsync(CurrentStockDetail CurrentStockDetail);
        Task DeleteCurrentStockDetailAsync(long id);
    }
}
