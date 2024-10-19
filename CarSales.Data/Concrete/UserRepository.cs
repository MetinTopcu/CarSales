using CarSales.Data.Abstract;
using CarSales.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarSales.Data.Concrete
{
    public class UserRepository : GenericRepository<User, CarDbContext, int>, IUserRepository
    {
        public UserRepository(CarDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> UserwithRoleListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.Include(x => x.Role).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> UserwithRoleListAsync(Expression<Func<User, bool>> selector, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.Include(x => x.Role).Where(selector).ToListAsync(cancellationToken);
        }

    }
}
