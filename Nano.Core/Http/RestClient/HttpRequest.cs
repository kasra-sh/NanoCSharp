using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Nano.Core.Http.RestClient
{
    public class HttpRequest
    {
        private HttpRequestMessage _request;
        private HttpClient _client;

        public HttpRequest(HttpRequestMessage request)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            _request = request;
            _request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            _client = new HttpClient(handler);
        }

        public async Task<HttpResponseMessage> Send()
        {
            var res = await _client.SendAsync(_request);
            return res;
        }

        public static HttpRequestBuilder Post(string url)
        {
            return new HttpRequestBuilder(HttpMethod.Post, url);
        }

        public static HttpRequestBuilder Get(string url)
        {
            return new HttpRequestBuilder(HttpMethod.Get, url);
        }

        public static HttpRequestBuilder Delete(string url)
        {
            return new HttpRequestBuilder(HttpMethod.Delete, url);
        }

        public static HttpRequestBuilder Put(string url)
        {
            return new HttpRequestBuilder(HttpMethod.Put, url);
        }
    }

}
