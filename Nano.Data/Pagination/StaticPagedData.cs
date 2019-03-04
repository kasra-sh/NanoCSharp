using System.Collections.Generic;

namespace Nano.Data.Pagination
{
    public class StaticPagedData<T>
    {
        public int Count { get; set; }

        public int TotalCount { get; set; }

        public int PageNum { get; set; }

        public List<T> Data { get; set; }
    }

}
