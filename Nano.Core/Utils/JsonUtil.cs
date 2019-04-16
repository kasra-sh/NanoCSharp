using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nano.Core.Utils
{
	public class JsonUtil
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

        public static JObject LoadObject(string json)
        {
            return JObject.Parse(json);
        }

        public static JArray LoadArray(string json)
        {
            return JArray.Parse(json);
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

        public static JObject LoadFileAsObject(string file)
        {
            return JObject.Parse(File.ReadAllText(file));
        }

        public static JArray LoadFileAsArray(string file)
        {
            return JArray.Parse(File.ReadAllText(file));
        }
    }
}
