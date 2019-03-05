using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano.Data.Entity;

namespace Nano.Data
{
    public class EfRepository<T> : IRepository where T: BaseEntity
    {
        private NanoDbContext _dbContext;
        private DbSet<T> _dbSet;
        private bool _autoSave = true;

        public EfRepository(NanoDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        
        public NanoDbContext GetDbContext()
        {
            return _dbContext;
        }

        public IQueryable<T> Table => _dbSet;

        public IQueryable<T> TableNoTracking => _dbSet.AsNoTracking();

        public DbResultLazy<T> Select(Func<T, bool> predicate)
        {
            try
            {
                var res = _dbSet.AsNoTracking().AsEnumerable().Where(predicate).AsQueryable();
                return ResultLazy(true, null, res);
            }
            catch (Exception e)
            {
                return ResultLazy(false, null, null, e);
            }
        }

        public async Task<DbResult<T>> SelectOneAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var res = await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
                return Result(true, res);
            }
            catch (Exception e)
            {
                return Result(false, null, null, e);
            }
        }

        public async Task<DbResult<T>> SelectAsync(Func<T, bool> predicate)
        {
            try
            {
                var res = await _dbSet.AsNoTracking().AsEnumerable().Where(predicate).AsQueryable().ToListAsync();
                return Result(true, null, res);
            }
            catch (Exception e)
            {
                return Result(false, null, null, e);
            }
        }

        public async Task<DbResult<T>> InsertAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                return await SaveChangesAsync(entity: entity);
            }
            catch (Exception e)
            {
                return Result(false, entity, null, e);
            }
        }

        public async Task<DbResult<T>> InsertAsync(ICollection<T> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                return await SaveChangesAsync(entities: entities);
            }
            catch (Exception e)
            {
                return Result(false, null, entities, e);
            }
        }

        public async Task<DbResult<T>> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                return await SaveChangesAsync(entity: entity);
            }
            catch (Exception e)
            {
                return Result(false, entity, null, e);
            }
        }

        public async Task<DbResult<T>> UpdateAsync(ICollection<T> entities)
        {
            try
            {
                _dbSet.UpdateRange(entities);
                return await SaveChangesAsync(entities: entities);
            }
            catch (Exception e)
            {
                return Result(false, null, entities, e);
            }
        }

        public async Task<DbResult<T>> RemoveAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return await SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Result(false, entity, null, e);
            }
        }

        public async Task<DbResult<T>> RemoveAsync(ICollection<T> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                return await SaveChangesAsync(null, entities);
            }
            catch (Exception e)
            {
                return Result(false, null, entities, e);
            }
        }

        public async Task<DbResult<T>> SaveChangesAsync(T entity = null, ICollection<T> entities = null)
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return Result(true, entity, entities);
            }
            catch (Exception e)
            {
                return Result(false, entity, entities, e);
            }
        }

        private static DbResult<T> Result(bool success, T item, ICollection<T> items = null, Exception e = null)
        {
            return new DbResult<T>
            {
                IsSuccess = success,
                Item = item,
                Items = items,
                ErrorMessage = e?.ToString() ?? (!success?"No changes saved!":null)
            };
        }

        private static DbResultLazy<T> ResultLazy(bool success, T item, IQueryable<T> items = null, Exception e = null)
        {
            return new DbResultLazy<T>
            {
                IsSuccess = success,
                Item = item,
                Items = items,
                ErrorMessage = e?.ToString() ?? (!success ? "No changes saved!" : null)
            };
        }
    }
}
