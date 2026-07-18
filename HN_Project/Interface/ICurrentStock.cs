using HN_Backend.Models;

namespace HN_Backend.Interface
{
    public interface ICurrentStock
    {
        Task<CurrentStock> GetCurrentStockByIdAsync(long id);
        Task <List<CurrentStock>>GetCurrentStockByProductIdAsync(long productId);
        Task CreateCurrentStockAsync(CurrentStock CurrentStock);
        Task UpdateCurrentStockAsync(CurrentStock CurrentStock);
        Task DeleteCurrentStockAsync(long id);
        Task DeleteCurrentStockByProductIdAsync(long productId);
    }
}
