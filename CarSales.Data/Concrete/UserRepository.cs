using CarSales.Data.Abstract;
using CarSales.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Data.Concrete
{
    public class UserRepository<TDbContext> : GenericRepository<User, TDbContext, int>, IUserRepository where TDbContext : DbContext
    {
        public UserRepository(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}
