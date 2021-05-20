using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly TaskManagementSystemDbContext _dbContext;

        public EfRepository(TaskManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            var data = await _dbContext.Set<T>().ToListAsync();
            return data;
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return await _dbContext.Set<T>().Where(filter).CountAsync();
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return false;
            }

            return await _dbContext.Set<T>().Where(filter).AnyAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            // Movie
            // Id PK,Identity
            // Title ="ABC", Revenue =1223321
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            // Movie
            // Id=22 PK,Identity
            // db data Title ="ABC", Revenue =1223321
            // Title = "ABC2", Revenue=5345435

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> ListAllWithIncludesAsync(Expression<Func<T, bool>> @where,
            params Expression<Func<T, object>>[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (includes != null)
                foreach (Expression<Func<T, object>> navigationProperty in includes)
                    query = query.Include(navigationProperty);

            return await query.Where(@where).ToListAsync();
        }
    }
}
