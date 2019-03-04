using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nano.WebFramework
{
    public class HttpActionResult: IActionResult
    {
        private readonly byte[] _bodyBytes;
        private Dictionary<string, string[]> _headers = new Dictionary<string, string[]>();

        public HttpActionResult(string body)
        {
            var _body = body ?? "";
            _bodyBytes = Encoding.UTF8.GetBytes(_body);
        }

        public HttpActionResult(byte[] body)
        {
            _bodyBytes = body ?? new byte[0];
        }

        public HttpActionResult SetHeader(string key, params string[] values)
        {
            _headers[key] = values;
            return this;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            foreach (var header in _headers)
            {
                context.HttpContext.Response.Headers.Add(header.Key, header.Value);
            }

            await context.HttpContext.Response.Body.WriteAsync(_bodyBytes, 0, _bodyBytes.Length);
        }
    }
}
