using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Nano.Core.RestClient.Xml;

namespace Nano.Core.Content
{
    public class XmlContent
    {
        public string ContentType => "application/xml";

        public string Serialize<T>(T obj) where T:class 
        {
            XmlSerializer xsSubmit = new XmlSerializer(obj.GetType());
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, obj);
                    xml = sww.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\""); ;
                    return xml;
                }
            }
        }

        public static string Generate(object obj)
        {
            return new XmlContent().Serialize(obj);
        }

        public static T Load<T>(string xml) where T: class
        {
            return new XmlContent().Deserialize<T>(xml);
        }

        public static string GenerateIndented(string xml)
        {
            if (!xml.Contains("<?xml"))
                return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n"+XDocument.Parse(xml);
            else
            {
                return XDocument.Parse(xml).ToString();
            }
        }

        public static string GenerateIndented(object xml)
        {
            return GenerateIndented(Generate(xml));
        }

        public T Deserialize<T>(string content) where  T: class
        {
//            XmlSerializer serializer = new XmlSerializer(typeof(T));
//            //            var strStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
//            //            Console.WriteLine(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(content)));
//            //            Console.WriteLine(content.Substring(672, 100));
//            //            var obj = xsSubmit.Map(strStream);
//            var reader = new StringReader(content);
////            reader.ReadToEnd();
//            return (T)serializer.Deserialize(reader);
            return NanoXmlMapper.Map<T>(content);
        }
    }
}
