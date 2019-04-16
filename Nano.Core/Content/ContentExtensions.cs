using Nano.Core.Utils;
using Newtonsoft.Json.Linq;

namespace Nano.Core.Content
{
    public static class ContentExtensions
    {
        public static string GenerateJson(this object o, bool indent = false)
        {
            return JsonUtil.Generate(o, indent);
        }

        public static T LoadJson<T>(this string str) where T: class
        {
            return JsonUtil.Load<T>(str);
        }

        public static T Get<T>(this JObject obj, string key)
        {
            return obj.GetValue(key).ToObject<T>();
        }
    }
}
