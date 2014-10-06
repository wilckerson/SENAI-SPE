using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Extensions.IEnumerableEx
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
