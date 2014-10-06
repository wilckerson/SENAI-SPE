using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common.Extensions.RegexUtilsEx;
using System.Text.RegularExpressions;

namespace Common.Extensions.StringEx
{
    /// <summary>
    /// Validation extensions for <see cref="System.String"/>
    /// </summary>
    public static class StringValidationExtensions
    {
        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid slug.
        /// </summary>
        public static bool IsValidSlug(this string value)
        {
            return Match(value, RegexUtilsEx.RegexUtilsEx.SlugRegex);
        }

        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid (absolute) URL.
        /// </summary>
        public static bool IsValidUrl(this string value) // absolute
        {
            return Match(value, RegexUtilsEx.RegexUtilsEx.UrlRegex);
        }

        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid Email Address.
        /// </summary>
        public static bool IsValidEmail(this string value)
        {
            return Match(value, RegexUtilsEx.RegexUtilsEx.EmailRegex);
        }

        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid IP Address.
        /// </summary>
        public static bool IsValidIPAddress(this string value)
        {
            return Match(value, RegexUtilsEx.RegexUtilsEx.IPAddressRegex);
        }

        private static bool Match(string value, Regex regex)
        {
            return value.IsNotNullOrEmpty() || regex.IsMatch(value);
        }
    }
}
