using Microsoft.EntityFrameworkCore;
using Nano.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
//            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//            {
////                var allTypes = assembly.GetTypes().Where(t => t.Name.Contains("TT"));
////                var entityTypes = assembly
////                    .GetTypes()
////                    .Where(t => t.IsSubclassOf(typeof(BaseEntity<>)));
////

//            }
            var entityTypes = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(BaseEntity));
            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }

    }

    public static class ReflectionHelpers
    {
        public static System.Type[] GetAllDerivedTypes(this System.AppDomain aAppDomain, System.Type aType)
        {
            var result = new List<System.Type>();
            var assemblies = aAppDomain.GetAssemblies().ToList();
            assemblies.AddRange(aAppDomain.ReflectionOnlyGetAssemblies());
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (aType.IsAssignableFrom(type) && !type.IsAbstract)
                        result.Add(type);
                }
            }
            return result.ToArray();
        }
    }
}
