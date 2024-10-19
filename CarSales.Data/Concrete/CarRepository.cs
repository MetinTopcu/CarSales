using CarSales.Data.Abstract;
using CarSales.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarSales.Data.Concrete
{
    public class CarRepository : GenericRepository<Car, CarDbContext, int>, ICarRepository
    {
        public CarRepository(CarDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Car?> CarwithBrandFirstOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cars.Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Car?>> CarwithBrandListAsync(Expression<Func<Car, bool>> selector, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cars.Include(x => x.Brand).Where(selector).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Car?>> CarwithBrandListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cars.Include(x => x.Brand).ToListAsync(cancellationToken);
        }
    }
}
