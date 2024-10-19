using CarSales.Entities;
using System.Linq.Expressions;

namespace CarSales.Data.Abstract
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<IEnumerable<User>> UserwithRoleListAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> UserwithRoleListAsync(Expression<Func<User, bool>> selector, CancellationToken cancellationToken = default);
    }
}
