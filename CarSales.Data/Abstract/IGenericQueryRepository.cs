using System.Linq.Expressions;
using CarSales.Entities;

namespace CarSales.Data.Abstract
{
    public interface IGenericQueryRepository<T, U> where T : IEntity<U> where U : struct
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(U id, CancellationToken cancellationToken = default);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default);
        Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default);
    }
}
