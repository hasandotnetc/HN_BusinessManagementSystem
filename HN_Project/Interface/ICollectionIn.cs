using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface ICollectionIn
    {
        Task<Collection> GetCollectionByIdAsync(long id);
        Task CreateCollectionAsync(Collection Collection);
        Task UpdateCollectionAsync(Collection Collection);
        Task DeleteCollectionAsync(long id);
    }
}
