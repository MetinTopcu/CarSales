
namespace CarSales.Data.Abstract
{
    public interface IUnitOfWork<TDbContext>
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
        void BeginTransaction();
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Commit();
        Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Rollback();
    }
}
