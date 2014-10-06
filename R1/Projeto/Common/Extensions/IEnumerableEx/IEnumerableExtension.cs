using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common.HandleException;

namespace Common.Extensions.IEnumerableEx
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> function)
        {
            foreach (T item in list)
            {
                function(item);
            }
        }

        /// <summary>
        /// Determines whether a collection is null or has no elements without having to enumerate the entire collection to get a count.  Uses LINQ.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// <c>true</c> if this list is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Convenience method for retrieving a specific page of items within a collection.
        /// </summary>
        /// <param name="pageIndex">The index of the page to get.</param>
        /// <param name="pageSize">The size of the pages.</param>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            Ensure.Argument.NotNull(source, "source");
            Ensure.Argument.Is(pageIndex >= 0, "The page index cannot be negative.");
            Ensure.Argument.Is(pageSize > 0, "The page size must be greater than zero.");

            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Validates that the enumerable is not null and contains items.
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <returns>A string that consists of the members of values delimited by the <paramref name="separator"/> string. If values has no members, the method returns null.</returns>
        public static string JoinOrDefault<T>(this IEnumerable<T> values, string separator)
        {
            Ensure.Argument.NotNullOrEmpty(separator, "separator");

            if (values == null)
                return default(string);

            return string.Join(separator, values);
        }

        //The Traverse extension can be added to types of IEnumerable<T>.
        //Returns T
        //Input Param
        //"fnRecurse" - delegate with one parameter T and returns IEnumerable<T>
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> fnRecurse)
        {
            foreach (T item in source)
            {
                yield return item;

                var seqRecurse = fnRecurse(item);

                if (seqRecurse != null)
                {

                    //Making Recursive call to Traverse using 
                    //results from the lambda expression 
                    foreach (T itemRecurse in Traverse(seqRecurse, fnRecurse))
                    {
                        yield return itemRecurse;
                    }
                }
            }
        }
    }
}
