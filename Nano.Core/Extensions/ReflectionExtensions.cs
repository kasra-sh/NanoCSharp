using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static Type[] GetAllDerivedTypes(this AppDomain aAppDomain, Type aType)
        {
            var result = new List<Type>();
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
