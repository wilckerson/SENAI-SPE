using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Common.HandleException;

namespace Common.Extensions.ModelState
{
    public static class ModelStateExtension
    {
        /// <summary>
        /// Converts the <paramref name="modelState"/> to a string dictionary that can be easily serialized.
        /// </summary>
        public static IDictionary<string, string> ToSerializableDictionary(this ModelStateDictionary modelState)
        {
            Ensure.Argument.NotNull(modelState, "modelState");

            var dictionary = (from k in modelState.Keys
                              from e in modelState[k].Errors
                              select new { k, e.ErrorMessage }).ToDictionary(x => x.k, x => x.ErrorMessage);

            return dictionary;
        }
    }
}
