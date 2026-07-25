using HN_Backend.Interface;
using HN_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class CurrentStockDetailRepository:ICurrentStockDetail
    {
        private readonly ApplicationDbContext _context;
        public CurrentStockDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CurrentStockDetail> GetCurrentStocDetailkByIdAsync(long id)
        {
            return await _context.CurrentStockDetails.FindAsync(id);
        }
        public async Task CreateCurrentStockDetailAsync(CurrentStockDetail CurrentStockDetail)
        {
            _context.CurrentStockDetails.AddAsync(CurrentStockDetail);
            //await _context.SaveChangesAsync();
        }
        public async Task UpdateCurrentStockDetailAsync(CurrentStockDetail CurrentStockDetail)
        {
            _context.Entry(CurrentStockDetail).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteCurrentStockDetailAsync(long id)
        {
            var CurrentStockDetail = await _context.CurrentStockDetails.FindAsync(id);
            if (CurrentStockDetail != null)
            {
                _context.CurrentStockDetails.Remove(CurrentStockDetail);
                //await _context.SaveChangesAsync();
                await Task.CompletedTask;
            }
        }

    }
}
