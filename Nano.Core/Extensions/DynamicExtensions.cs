using System;
using System.Collections.Generic;
using System.Text;

namespace Nano.Core.Extensions
{
    public static class Dynamic
    {
        public static T MapTo<T>(dynamic obj) where T: class
        {
            return (obj as object).MapTo<T>();
        }

        public static dynamic MapFrom(object obj)
        {
            return obj.MapTo<dynamic>();
        }
    }
}
