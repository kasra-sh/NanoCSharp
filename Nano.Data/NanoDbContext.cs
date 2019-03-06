using Microsoft.EntityFrameworkCore;
using Nano.Data.Entity;
using System;
using Nano.Core.Extensions;

namespace Nano.Data
{
    public class NanoDbContext: DbContext
    {
        public NanoDbContext(DbContextOptions<NanoDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(BaseEntity));

            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }

    }

    
}
