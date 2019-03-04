using Microsoft.AspNetCore.Http;

namespace Nano.WebFramework
{
    public class RequestContext: IRequestContext
    {
        private readonly HttpContext _httpContext;

        public RequestContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string[] GetHeader(string headerKey)
        {
            var h = _httpContext.Request.Headers[headerKey];
            return h;
        }
    }
}
