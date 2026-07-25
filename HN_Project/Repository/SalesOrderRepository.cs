using HN_Backend.Interface;
using HN_Backend.Data; 

namespace HN_Backend.Repository
{
    public class SalesOrderRepository : ISalesOrder
    {
        private readonly ApplicationDbContext _context;
        public SalesOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SalesOrder> GetSalesOrderByIdAsync(long id)
        {
            return await _context.SalesOrders.FindAsync(id);
        }
        public async Task CreateSalesOrderAsync(SalesOrder salesOrder)
        {
            await _context.SalesOrders.AddAsync(salesOrder);
            //_context.SalesOrders.Add(salesOrder);
            //await _context.SaveChangesAsync();
        }
        public async Task UpdateSalesOrderAsync(SalesOrder salesOrder)
        {
            _context.Entry(salesOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteSalesOrderAsync(long id)
        {
            var salesOrder = await _context.SalesOrders.FindAsync(id);
            if (salesOrder != null)
            {
                _context.SalesOrders.Remove(salesOrder);
                //await _context.SaveChangesAsync();
                await Task.CompletedTask;
            }
        }
    }
}
