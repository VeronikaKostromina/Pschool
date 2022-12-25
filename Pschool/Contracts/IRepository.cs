using System.Linq.Expressions;

namespace Pschool.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task RemoveAsync(object key);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> FindAll(bool asNoTracking = true);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter, bool asNoTracking = true);
        Task<TEntity?> FindByKeyAsync(object key);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}
