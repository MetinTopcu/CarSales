using CarSales.Data.Concrete;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Service.Concrete
{
    public class Service<T, TDbContext> : GenericRepository<T, TDbContext, int>, IService<T, TDbContext> where T : class, IEntity<int>, new() where TDbContext : DbContext
    {
        public Service(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}
