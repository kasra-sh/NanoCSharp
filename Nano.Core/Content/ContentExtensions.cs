namespace Nano.Core.Content
{
    public static class ContentExtensions
    {
        public static string GenerateJson(this object o, bool indent = false)
        {
            return JsonContent.Generate(o, indent);
        }

        public static T LoadJson<T>(this string str) where T: class
        {
            return JsonContent.Load<T>(str);
        }
    }
}
