using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nano.Core.Http
{
    public class ApiResponse<T>
    {
        [JsonIgnore]
        public static ApiResponseBuilder Builder => new ApiResponseBuilder();

        public List<string> Errors { get; set; }

        public bool Success { get; set; }

        public T Data { get; set; }
    }
}
