using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Commons.Extensions
{
    public static class ListExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> function)
        {
            if (list != null)
            {
                foreach (T item in list)
                {
                    function(item);
                }
            }
        }
    }
}
