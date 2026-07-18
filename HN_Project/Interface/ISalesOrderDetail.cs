using HN_Backend.Models;

namespace HN_Backend.Interface
{
    public interface ISalesOrderDetail
    {
        Task<SalesOrderDetail> GetSalesOrderDetailByIdAsync(long id);
        Task CreateSalesOrderDetailAsync(SalesOrderDetail salesOrderDetail);
        Task UpdateSalesOrderDetailAsync(SalesOrderDetail salesOrderDetail);
        Task DeleteSalesOrderDetailAsync(long id);
    }
}
