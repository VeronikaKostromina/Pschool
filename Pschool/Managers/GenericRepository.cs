using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pschool.Contracts;

namespace Pschool.Managers
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task RemoveAsync(object key)
        {
            Remove(await FindByKeyAsync(key));
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public IQueryable<TEntity> FindAll(bool asNoTracking = true)
        {
            return asNoTracking ? dbSet.AsNoTracking() : dbSet;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter, bool asNoTracking = true)
        {
            return asNoTracking ? dbSet.Where(filter).AsNoTracking() : dbSet.Where(filter);
        }

        public async Task<TEntity> FindByKeyAsync(object key)
        {
            return await dbSet.FindAsync(key);
        }
    }
}
