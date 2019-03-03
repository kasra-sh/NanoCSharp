using System;
using Newtonsoft.Json;

namespace Nano.Core.Content
{
	public class JsonContent
    {
        public string ContentType => "application/json";

        public static string Generate(object obj, bool indent = false)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, indent?Formatting.Indented:Formatting.None);
        }

        public static T Load<T>(string json, bool throwException = true) where T: class
        {
            try
            {
                var aa = JsonConvert.DeserializeObject<T>(json);
                return aa;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (throwException)
                    throw;
                return null;
            }
        }

        public string Serialize<T>(T obj) where T: class
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string content) where T: class 
        {
            try
            {
                var aa = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                return aa;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
