using CarSales.Entities;

namespace CarSales.Data.Abstract
{
    public interface IGenericRepository<T, U> : IGenericQueryRepository<T, U>, IGenericCudRepository<T, U> where T : IEntity<U> where U : struct
    {
    }
}
