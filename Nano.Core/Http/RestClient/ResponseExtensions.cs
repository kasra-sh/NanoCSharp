using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Nano.Core.Content;
using Newtonsoft.Json.Linq;

namespace Nano.Core.RestClient
{
    public static class ResponseExtensions
    {
        public static async Task<JObject> AsJObject(this Task<HttpResponseMessage> message)
        {
            return JObject.Parse(await message.GetString());
        }

        public static async Task<JArray> AsJArray(this Task<HttpResponseMessage> message)
        {
            return JArray.Parse(await message.GetString());
        }

        public static async Task<T> ReadJson<T>(this Task<HttpResponseMessage> message)
        {
            return JToken.Parse(await message.GetString()).ToObject<T>();
        }

        public static async Task<HttpFullResponse<T>> ReadJsonFull<T>(this Task<HttpResponseMessage> message, bool getString = true) where T : class
        {
            var msg = await message;
            var res = await msg.Content.ReadAsByteArrayAsync();
            string resString = null;
            if (getString)
            {
                resString = Encoding.UTF8.GetString(res);
            }
            return new HttpFullResponse<T>
            (
                resString,
                res,
                JsonContent.Load<T>(resString),
                msg.StatusCode
            );
        }

        public static async Task<T> ReadXml<T>(this Task<HttpResponseMessage> message) where T : class
        {
            return XmlContent.Load<T>(await message.GetString());
        }

        public static async Task<T> ReadParsianXml<T>(this Task<HttpResponseMessage> message, string rootName)
            where T : class
        {
            var xDoc = XDocument.Parse(await message.GetString());
            var newXDoc = new XDocument(new XElement(rootName, xDoc.Root));
            return XmlContent.Load<T>(newXDoc.ToString());
        }

        public static async Task<string> GetString(this Task<HttpResponseMessage> message)
        {
            return await (await message).Content.ReadAsStringAsync();
        }

        public static async Task<byte[]> GetBytes(this Task<HttpResponseMessage> message)
        {
            return await (await message).Content.ReadAsByteArrayAsync();
        }

        public static async Task<XDocument> AsXDocument(this Task<HttpResponseMessage> message)
        {
            return XDocument.Parse(await message.GetString());
        }
    }
}