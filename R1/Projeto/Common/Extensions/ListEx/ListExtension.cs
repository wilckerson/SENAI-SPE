using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace Common.Extensions.ListEx
{
    public static class ListExtension
    {
        /// <summary>
        /// Return from IList items to CSV file data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToCsv<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                sb.Append(prop.Name);
                if (i < props.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(Environment.NewLine);
            var values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                    sb.Append(values[i].ToString());
                    if (i < values.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Return the file delimited by delimiter parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="logfile"></param>
        /// <param name="delimiter"></param>
        public static void WriteDelimitedFile<T>(this IEnumerable<T> list, string logfile, string delimiter)
        {
            using (var sw = File.AppendText(logfile))
            {
                var props = typeof(T).GetProperties();
                var headerNames = props.Select(x => x.Name);
                sw.WriteLine(string.Join(delimiter, headerNames.ToArray()));
                foreach (var item in list)
                {
                    var item1 = item;
                    var values = props
                    .Select(x => x.GetValue(item1, null) ?? "")
                    .Select(x => x.ToString())
                    .Select(x => x.Contains(delimiter) || x.Contains("\n") ? "\"" + x + "\"" : x);
                    sw.WriteLine(string.Join(delimiter, values.ToArray()));
                }
                sw.Close();
            }
        }

        public static void ForEach<T>(this IList<T> list, Action<T> function)
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
        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
            return items == null || !items.Any();
        }
    }
}
