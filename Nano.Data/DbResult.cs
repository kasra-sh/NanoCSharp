using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Data
{
    public class DbResultBase<T>
    {
        public T Item { get; set; }
        
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class DbResultLazy<T>:DbResultBase<T>
    {
        public IQueryable<T> Items { get; set; }
    }

    public class DbResult<T> : DbResultBase<T>
    {
        public ICollection<T> Items { get; set; }
    }
}
