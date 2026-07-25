using HN_Backend.Interface;
using HN_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class CollectionDetailRepository: ICollectionDetail
    {
        private readonly ApplicationDbContext _context;
        public CollectionDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CollectionDetail> GetCollectionDetailByIdAsync(long id)
        {
            return await _context.CollectionDetails.FindAsync(id);
        }
        public async Task CreateCollectionDetailAsync(CollectionDetail collectionDetail)
        {
            _context.CollectionDetails.AddAsync(collectionDetail);
            //_context.CollectionDetails.Add(collectionDetail);
            //await _context.SaveChangesAsync();
        }
        public async Task UpdateCollectionDetailAsync(CollectionDetail collectionDetail)
        {

            //_context.CollectionDetails.Attach(collectionDetail);

            //// 2. Explicitly mark ONLY this specific entity as modified.
            //// This tells EF Core: "Only update the columns of CollectionDetail. Do NOT touch the Bank or Collection tables."
            _context.Entry(collectionDetail).State = EntityState.Modified;

            await Task.CompletedTask;


            // It automatically handles attached/detached states and schedules the update.
            //_context.CollectionDetails.Update(collectionDetail);
             
            //await Task.CompletedTask;

              
            //_context.Entry(collectionDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //await _context.SaveChangesAsync();
        }
        public async Task DeleteCollectionDetailAsync(long id)
        {
            var collectionDetail = await _context.CollectionDetails.FindAsync(id);
            if (collectionDetail != null)
            {
                _context.CollectionDetails.Remove(collectionDetail);
                await Task.CompletedTask;
                //await _context.SaveChangesAsync();
            }
        }

    }
}
