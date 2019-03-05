using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nano.Data.Entity;

namespace Nano.Web
{
    public class YY: BaseEntity
    {
        public long TTId { get; set; }

        public string Description { get; set; }

        public virtual TT TT { get; set; }
    }
}
