using System;
using System.Collections.Generic;
using System.Linq;

namespace Bedrock.DomainBuilder
{
    public static class EnumerableExtensions
    {
        #region Extension Methods
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var i in items)
                action(i);
        }

        public static void Each<T>(this IEnumerable<T> items, Func<T, bool> function)
        {
            foreach (var i in items)
                if (!function(i))
                    break;
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            var itemList = items.ToArray();

            for (int i = 0; i < itemList.Count(); i++)
                action(itemList[i], i);
        }

        public static void Each<T>(this IEnumerable<T> items, Func<T, int, bool> function)
        {
            var itemList = items.ToArray();

            for (int i = 0; i < itemList.Count(); i++)
                if (!function(itemList[i], i))
                    break;
        }

        public static string ToDelimitedString(this IEnumerable<string> value, string delimeter)
        {
            return string.Join(delimeter, value);
        }
        #endregion
    }
}
