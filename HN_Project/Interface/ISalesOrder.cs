using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface ISalesOrder
    {
        Task<SalesOrder> GetSalesOrderByIdAsync(long id);
        Task CreateSalesOrderAsync(SalesOrder salesOrder);
        Task UpdateSalesOrderAsync(SalesOrder salesOrder);
        Task DeleteSalesOrderAsync(long id);
    }
}
