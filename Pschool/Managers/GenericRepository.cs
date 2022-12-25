using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Managers
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity<long>
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task RemoveAsync(object key)
        {
            var entity = await FindByKeyAsync(key);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
            context.Entry(entity).Property(x => x.Created).IsModified = false;
        }

        public IQueryable<TEntity> FindAll(bool asNoTracking = true)
        {
            return asNoTracking ? dbSet.AsNoTracking() : dbSet;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter, bool asNoTracking = true)
        {
            return asNoTracking ? dbSet.Where(filter).AsNoTracking() : dbSet.Where(filter);
        }

        public async Task<TEntity?> FindByKeyAsync(object key)
        {
            return await dbSet.FindAsync(key);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }
    }
}
