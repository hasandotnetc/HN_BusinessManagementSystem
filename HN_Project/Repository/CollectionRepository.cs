using HN_Backend.Interface;
using HN_Backend.Models; 
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HN_Backend.Repository
{
    public class CollectionRepository: ICollectionIn
    {
        private readonly ApplicationDbContext _context; 
        public CollectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Collection> GetCollectionByIdAsync(long id)
        {
            return await _context.Collections.FindAsync(id);
        }
        public async Task CreateCollectionAsync(Collection collection)
        {
            _context.Collections.AddAsync(collection);
            //await _context.SaveChangesAsync();
        }
        public async Task UpdateCollectionAsync(Collection collection)
        {
            _context.Entry(collection).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            await Task.CompletedTask; 
             

        }
        public async Task DeleteCollectionAsync(long id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
                await Task.CompletedTask;
                //await _context.SaveChangesAsync();
            }
        }
         
    }
}
