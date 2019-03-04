using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano.Data.Entity;

namespace Nano.Data
{
    public class EfRepository<T> : IEfRepository where T: BaseEntity
    {
        private NanoDbContext _dbContext;
        private DbSet<T> _dbSet;

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

        public async Task<DbResult<T>> SelectOneAsync(Func<T, bool> predicate)
        {
            try
            {
                var res = await _dbSet.FirstOrDefaultAsync(arg => predicate.Invoke(arg));
                
                return new DbResult<T>
                {
                    IsSuccess = true,
                    Item = res
                };
            }
            catch (Exception e)
            {
                return new DbResult<T>
                {
                    IsSuccess = false,
                    ErrorMessage = e.ToString()
                };
            }
        }

        public async Task<DbResult<T>> SelectAsync(Func<T, bool> predicate)
        {
            try
            {
                var res = await _dbSet.Where(predicate).AsQueryable().ToListAsync();

                return new DbResult<T>
                {
                    IsSuccess = true,
                    Items = res
                };
            }
            catch (Exception e)
            {
                return new DbResult<T>
                {
                    IsSuccess = false,
                    ErrorMessage = e.ToString()
                };
            }
        }
    }
}
