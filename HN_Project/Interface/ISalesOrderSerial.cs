using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface ISalesOrderSerial
    {
        Task<SalesOrderSerial> GetSalesOrderSerialByIdAsync(long id);
        Task CreateSalesOrderSerialAsync(SalesOrderSerial SalesOrderSerial);
        Task UpdateSalesOrderSerialAsync(SalesOrderSerial SalesOrderSerial);
        Task DeleteSalesOrderSerialAsync(long id);
    }
}
