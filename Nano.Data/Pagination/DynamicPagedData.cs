using System.Collections.Generic;

namespace Nano.Data.Pagination
{
    public class DynamicPagedData<T>
    {
        public int Count { get; set; }

        public string NextPageToken { get; set; }

        public bool HasNext { get; set; }

        public List<T> Data { get; set; }
    }
}
