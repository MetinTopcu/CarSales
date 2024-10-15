using CarSales.Entities;
using System.Linq.Expressions;

namespace CarSales.Data.Abstract
{
    public interface ICarRepository : IGenericRepository<Car, int>
    {
        Task<Car?> CarwithBrandFirstOrDefaultAsync (int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Car?>> CarwithBrandListAsync(Expression<Func<Car, bool>> selector, CancellationToken cancellationToken = default);
        Task<IEnumerable<Car?>> CarwithBrandListAsync (CancellationToken cancellationToken = default);
    }
}
