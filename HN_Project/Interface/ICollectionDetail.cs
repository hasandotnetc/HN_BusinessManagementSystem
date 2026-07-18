using HN_Backend.Models;

namespace HN_Backend.Interface
{
    public interface ICollectionDetail
    {
        Task<CollectionDetail> GetCollectionDetailByIdAsync(long id);
        Task CreateCollectionDetailAsync(CollectionDetail CollectionDetail);
        Task UpdateCollectionDetailAsync(CollectionDetail CollectionDetail);
        Task DeleteCollectionDetailAsync(long id);
    }
}
