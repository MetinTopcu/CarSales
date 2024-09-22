using CarSales.Data.Abstract;
using CarSales.Entities;

namespace CarSales.Service.Abstract
{
    public interface IService<T, TDbContext> : IGenericRepository<T, int> where T : class, IEntity<int>, new()
    {
    }
}
