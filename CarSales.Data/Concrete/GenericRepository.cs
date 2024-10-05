using CarSales.Data.Abstract;
using CarSales.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarSales.Data.Concrete
{
    public abstract class GenericRepository<T, TDbContext, U> : IGenericRepository<T, U> where T : class,IEntity<U> where TDbContext : DbContext where U : struct
    {

        protected readonly TDbContext _dbContext;
        private readonly DbSet<T> _dbSet;


        protected GenericRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default)
        {
            return await Where(selector).ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(U id, CancellationToken cancellationToken = default)
        {
            return await SingleOrDefaultAsync((T x) => x.Id.Equals(id),cancellationToken);
        }
        public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default)
        {
            return (selector != null) ? await _dbSet.SingleOrDefaultAsync(selector, cancellationToken) : (await _dbSet.SingleOrDefaultAsync(cancellationToken));
        }
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default)
        {
            return (selector != null) ? await _dbSet.FirstOrDefaultAsync(selector, cancellationToken) : (await _dbSet.FirstOrDefaultAsync(cancellationToken));
        }
        public virtual async Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default)
        {
            return (selector != null) ? await _dbSet.OrderBy((T x) => x.Id).LastOrDefaultAsync(selector, cancellationToken) : (await _dbSet.FirstOrDefaultAsync(cancellationToken));
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default)
        {
            return (selector != null) ? await _dbSet.CountAsync(selector, cancellationToken) : (await _dbSet.CountAsync(cancellationToken));
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default)
        {
            return (selector != null) ? (await _dbSet.AnyAsync(selector, cancellationToken)) : (await _dbSet.AnyAsync(cancellationToken));
        }

        public virtual async Task<T> InsertOneAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> InsertManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            return entities;
        }
        public virtual void UpdateOne(T entity)
        {
            _dbSet.Update(entity);
        }
        public virtual void UpdateMany(IEnumerable<T> entities)
        {
            foreach(T entity in entities)
            {
                _dbSet.Update(entity);
            }
        }
        public virtual void DeleteOne(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteMany(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        private IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> selector)
        {
            return _dbSet.Where(selector);
        }
    }
}
