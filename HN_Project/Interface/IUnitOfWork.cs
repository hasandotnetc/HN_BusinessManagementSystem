namespace HN_Backend.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> CommitAsync();
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
