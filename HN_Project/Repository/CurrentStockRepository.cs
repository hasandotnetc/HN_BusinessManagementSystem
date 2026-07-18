using HN_Backend.Interface;
using HN_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class CurrentStockRepository:ICurrentStock
    {
        private readonly ApplicationDbContext _context;
        public CurrentStockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CurrentStock> GetCurrentStockByIdAsync(long id)
        {
            return await _context.CurrentStocks.FindAsync(id);
        }
        public async Task <List<CurrentStock>> GetCurrentStockByProductIdAsync(long productId)
        {
            //return await _context.CurrentStocks.FindAsync(productId);
            return await _context.CurrentStocks.Where(x => x.ProductId == productId).ToListAsync(); 
        } 

        public async Task CreateCurrentStockAsync(CurrentStock currentStock)
        {
            _context.CurrentStocks.AddAsync(currentStock);
            //await _context.SaveChangesAsync();
        }
        public async Task UpdateCurrentStockAsync(CurrentStock currentStock)
        {
            _context.CurrentStocks.Update(currentStock);
            //await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteCurrentStockAsync(long id)
        {
            var currentStock = await _context.CurrentStocks.FindAsync(id);
            if (currentStock != null)
            {
                _context.CurrentStocks.Remove(currentStock);
                //await _context.SaveChangesAsync();
                await Task.CompletedTask;
            }
        }

        public async Task DeleteCurrentStockByProductIdAsync(long productId)
        {
            var currentStock = await _context.CurrentStocks.FindAsync(productId);
            if (currentStock != null)
            {
                _context.CurrentStocks.Remove(currentStock);
                //await _context.SaveChangesAsync();
                await Task.CompletedTask;
            }
        }

    }
}
