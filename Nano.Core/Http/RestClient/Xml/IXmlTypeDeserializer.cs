namespace Nano.Core.Http.RestClient.Xml
{
    public interface IXmlTypeDeserializer<out T> where T: class 
    {
        T Convert(string xmlValue);
    }

}
