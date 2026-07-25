using HN_Backend.Interface;
using HN_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class SalesOrderDetailSerialRepository : ISalesOrderSerial
    {
        private readonly ApplicationDbContext _context;
        public SalesOrderDetailSerialRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SalesOrderSerial> GetSalesOrderSerialByIdAsync(long id)
        {
            return await _context.SalesOrderSerials.FindAsync(id);
        }
        public async Task CreateSalesOrderSerialAsync(SalesOrderSerial SalesOrderSerial)
        {
            _context.SalesOrderSerials.AddAsync(SalesOrderSerial);
            //await _context.SaveChangesAsync();
        }
        public async Task UpdateSalesOrderSerialAsync(SalesOrderSerial SalesOrderSerial)
        {
            _context.Entry(SalesOrderSerial).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteSalesOrderSerialAsync(long id)
        {
            var SalesOrderSerial = await _context.SalesOrderSerials.FindAsync(id);
            if (SalesOrderSerial != null)
            {
                _context.SalesOrderSerials.Remove(SalesOrderSerial);
                //await _context.SaveChangesAsync();
                await Task.CompletedTask;
            }
        }

    }
}
