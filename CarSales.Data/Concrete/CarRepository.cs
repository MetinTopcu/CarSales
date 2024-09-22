using CarSales.Data.Abstract;
using CarSales.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Data.Concrete
{
    public class CarRepository<TDbContext> : GenericRepository<Car, TDbContext, int>, ICarRepository where TDbContext : DbContext
    {
        public CarRepository(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}
