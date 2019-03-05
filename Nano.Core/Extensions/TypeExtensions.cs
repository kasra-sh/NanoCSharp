using Newtonsoft.Json.Linq;

namespace Nano.Core.Extensions
{
    public static class TypeExtensions
    {
        public static TTarget MapTo<TTarget>(this object obj) where TTarget: class
        {
            return JToken.FromObject(obj).ToObject<TTarget>();
        }
    }
}
