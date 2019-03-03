namespace Nano.Core.RestClient.Xml
{
    public interface IXmlTypeDeserializer<out T> where T: class 
    {
        T Convert(string xmlValue);
    }

}
