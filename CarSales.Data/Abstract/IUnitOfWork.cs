
namespace CarSales.Data.Abstract
{
    public interface IUnitOfWork<TDbContext>
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task CommitAsync(CancellationToken cancellationToken = default);
        void Commit();
        Task RollbackAsync(CancellationToken cancellationToken = default);
        void Rollback();
    }
}
