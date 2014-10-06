using Common.HandleException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Extensions.TypeEx
{
    /// <summary>
    /// Extension method for idetification whether the type implements other
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Method for identification whether the type implements other type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Implements<T>(this Type type)
        {
            Ensure.Argument.NotNull(type, "type");
            return typeof(T).IsAssignableFrom(type);
        }
    }
}
