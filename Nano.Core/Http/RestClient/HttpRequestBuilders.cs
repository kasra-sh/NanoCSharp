using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Nano.Core.Content;
using Newtonsoft.Json.Linq;

namespace Nano.Core.RestClient
{

    public class HttpRequestBuilder
    {
        private readonly HttpRequestMessage _request;
        internal HttpRequestBuilder(HttpMethod method, string url)
        {
            _request = new HttpRequestMessage(method, url);
            _request.Headers.Add("Accept-Encoding", "gzip, deflate");
        }

        public HttpRequestBuilder ContentType(string contentType)
        {
            _request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            return this;
        }

        public HttpRequestBuilder Authorization(string scheme, string parameter)
        {
            _request.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }

        public HttpRequestBuilder AddHeader(string key, params string[] value)
        {
            _request.Headers.Add(key, value);
            return this;
        }

        public HttpRequestBuilder Content(string contentType, byte[] content)
        {
            _request.Content = new ByteArrayContent(content);
            _request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return this;
        }

        public HttpRequestBuilder Content(string contentType, string content)
        {
            _request.Content = new StringContent(content);
            _request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return this;
        }

        public HttpRequestBuilder JsonContent(string json)
        {
            return Content(ContentTypes.ApplicationJson, Encoding.UTF8.GetBytes(json));
        }

        public HttpRequestBuilder JsonContent(object json)
        {
            return Content(ContentTypes.ApplicationJson, Encoding.UTF8.GetBytes(JToken.FromObject(json).ToString()));
        }

        public HttpRequestBuilder XmlContent(string xml)
        {
            return Content(ContentTypes.ApplicationXml, Encoding.UTF8.GetBytes(xml));
        }

        public HttpRequestBuilder XmlContent(object xml)
        {
            return Content(ContentTypes.ApplicationXml, Encoding.UTF8.GetBytes(new XmlContent().Serialize(xml)));
        }

        public UrlEncodedContentBuilder UrlEncodedContent()
        {
            return new UrlEncodedContentBuilder(_request);
        }

        public FormDataContentBuilder FormDataContent()
        {
            return new FormDataContentBuilder(_request);
        }

        public HttpRequest Build()
        {
            return new HttpRequest(_request);
        }

        public async Task<HttpResponseMessage> Send()
        {
            return await Build().Send();
        }
    }

    public class UrlEncodedContentBuilder
    {
        private readonly HttpRequestMessage _request;
        private IList<KeyValuePair<string, string>> _keyValuePairs = new List<KeyValuePair<string, string>>();
        
        internal UrlEncodedContentBuilder(HttpRequestMessage request)
        {
            _request = request;
        }

        public UrlEncodedContentBuilder AddParam(string key, string value)
        {
            _keyValuePairs.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public HttpRequest Build()
        {
            _request.Content = new FormUrlEncodedContent(_keyValuePairs);
            return new HttpRequest(_request);
        }
        public async Task<HttpResponseMessage> Send()
        {
            return await Build().Send();
        }

    }

    public class FormDataContentBuilder
    {
        private readonly HttpRequestMessage _request;
        private MultipartFormDataContent _multipart;

        internal FormDataContentBuilder(HttpRequestMessage request)
        {
            _request = request;
            _multipart = new MultipartFormDataContent();
        }

        public FormDataContentBuilder Add(HttpContent content)
        {
            _multipart.Add(content);
            return this;
        }

        public FormDataContentBuilder Add(HttpContent content, string name)
        {
            _multipart.Add(content, name);
            return this;
        }

        public FormDataContentBuilder AddString(string content, string name)
        {
            _multipart.Add(new StringContent(content, Encoding.UTF8), name);
            return this;
        }

        public FormDataContentBuilder AddFile(byte[] content, string contentType, string name, string fileName)
        {
            var c = new ByteArrayContent(content);
            c.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            _multipart.Add(c, name, fileName);
            return this;
        }

        public HttpRequest Build()
        {
            _request.Content = _multipart;
            return new HttpRequest(_request);
        }

        public async Task<HttpResponseMessage> Send()
        {
            return await Build().Send();
        }

    }

}
