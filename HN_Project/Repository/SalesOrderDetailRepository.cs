using HN_Backend.Interface;
using HN_Backend.Models; 
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class SalesOrderDetailDetail: ISalesOrderDetail
    {
        private readonly ApplicationDbContext _context; 
        public SalesOrderDetailDetail(ApplicationDbContext context)
        {
            _context = context;
        }
         
        public async Task CreateSalesOrderDetailAsync(SalesOrderDetail SalesOrderDetail)
        {
            _context.SalesOrderDetails.AddAsync(SalesOrderDetail);
            //await _context.SaveChangesAsync();
        }
        public async Task UpdateSalesOrderDetailAsync(SalesOrderDetail SalesOrderDetail)
        {
            _context.Entry(SalesOrderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteSalesOrderDetailAsync(long id)
        {
            var SalesOrderDetail = await _context.SalesOrderDetails.FindAsync(id);
            if (SalesOrderDetail != null)
            {
                _context.SalesOrderDetails.Remove(SalesOrderDetail);
                //await _context.SaveChangesAsync();
                await Task.CompletedTask;
            }
        }

        public async Task<SalesOrderDetail> GetSalesOrderDetailByIdAsync(long id)
        {
            return await _context.SalesOrderDetails.Where(x=>x.SalesOrderDetailId == id).FirstOrDefaultAsync();
        }
    }
}
