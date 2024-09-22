using CarSales.Entities;

namespace CarSales.Data.Abstract
{
    public interface IGenericCudRepository<T, U> where T : IEntity<U> where U : struct
    {
        Task<T> InsertOneAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> InsertManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void UpdateOne(T entity);
        void UpdateMany(IEnumerable<T> entities);
        void DeleteOne(T entity);
        void DeleteMany(IEnumerable<T> entities);
    }
}
