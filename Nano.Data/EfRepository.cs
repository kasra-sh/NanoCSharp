using System;
using System.Collections.Generic;
using System.Text;
using Nano.Data.Entity;

namespace Nano.Data
{
    public class EfRepository<T, IdType> : IEfRepository where T: BaseEntity
    {
        private NanoDbContext _dbContext;

        public EfRepository(NanoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public NanoDbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
