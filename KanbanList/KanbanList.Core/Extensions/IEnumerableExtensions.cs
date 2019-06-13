using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KanbanList.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
     (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null ? true : !enumerable.Any();
        }

        public static void AddRange<T>(this ICollection<T> enumerable, IEnumerable<T> range)
        {
            if (range == null)
            {
                return;
            }

            foreach (T item in range)
            {
                enumerable.Add(item);
            }
        }


        public static void AddRange(this IList enumerable, IEnumerable range)
        {
            if (range == null)
            {
                return;
            }

            foreach (object item in range)
            {
                enumerable.Add(item);
            }
        }
    }
}
