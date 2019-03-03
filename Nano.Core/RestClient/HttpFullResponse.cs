using System.Net;

namespace Nano.Core.RestClient
{
    public class HttpFullResponse<T>
    {
        public HttpFullResponse(string rawResponse, byte[] rawResponseBytes, T response, HttpStatusCode status)
        {
            RawResponse = rawResponse;
            RawResponseBytes = rawResponseBytes;
            Response = response;
            Status = status;
        }

        public string RawResponse { get; set; }
        public byte[] RawResponseBytes { get; set; }
        public T Response { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
