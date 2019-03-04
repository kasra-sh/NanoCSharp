using System;
using System.Collections.Generic;
using System.Text;

namespace Nano.Data
{
    public class DbResult<T>
    {
        public T Item { get; set; }

        public IEnumerable<T> Items { get; set; }
        
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
