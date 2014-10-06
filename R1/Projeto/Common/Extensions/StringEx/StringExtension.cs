using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;

using Common.HandleException;
using Common.Extensions;

namespace Common.Extensions.StringEx
{
    public static class StringExtension
    {
        private static MD5CryptoServiceProvider s_md5 = null;

        public static T ConvertToEnum<T>(this string value)
        {
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }
            if (Enum.IsDefined(typeof(T), value) == false)
            {
                throw new InvalidCastException();
            }
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// A nicer way of calling <see cref="System.String.IsNullOrEmpty(string)"/>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// A nicer way of calling the inverse of <see cref="System.String.IsNullOrEmpty(string)"/>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is not null or an empty string (""); otherwise, false.</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        /// <summary>
        /// Allows for using strings in null coalescing operations
        /// </summary>
        /// <param name="value">The string value to check</param>
        /// <returns>Null if <paramref name="value"/> is empty or the original value of <paramref name="value"/>.</returns>
        public static string NullIfEmpty(this string value)
        {
            if (value == string.Empty)
                return null;

            return value;
        }

        /// <summary>
        /// Slugifies a string
        /// </summary>
        /// <param name="value">The string value to slugify</param>
        /// <param name="maxLength">An optional maximum length of the generated slug</param>
        /// <returns>A URL safe slug representation of the input <paramref name="value"/>.</returns>
        public static string ToSlug(this string value, int? maxLength = null)
        {
            Ensure.Argument.NotNull(value, "value");

            // if it's already a valid slug, return it
            if (RegexUtilsEx.RegexUtilsEx.SlugRegex.IsMatch(value))
                return value;

            return GenerateSlug(value, maxLength);
        }

        /// <summary>
        /// Converts a string into a slug that allows segments e.g. <example>.blog/2012/07/01/title</example>.
        /// Normally used to validate user entered slugs.
        /// </summary>
        /// <param name="value">The string value to slugify</param>
        /// <returns>A URL safe slug with segments.</returns>
        public static string ToSlugWithSegments(this string value)
        {
            Ensure.Argument.NotNull(value, "value");

            var segments = value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var result = segments.Aggregate(string.Empty, (slug, segment) => slug += "/" + segment.ToSlug());
            return result.Trim('/');
        }

        /// <summary>
        /// Separates a PascalCase string
        /// </summary>
        /// <example>
        /// "ThisIsPascalCase".SeparatePascalCase(); // returns "This Is Pascal Case"
        /// </example>
        /// <param name="value">The value to split</param>
        /// <returns>The original string separated on each uppercase character.</returns>
        public static string SeparatePascalCase(this string value)
        {
            Ensure.Argument.NotNullOrEmpty(value, "value");
            return Regex.Replace(value, "([A-Z])", " $1").Trim();
        }

        /// <summary>
        /// Credit for this method goes to http://stackoverflow.com/questions/2920744/url-slugify-alrogithm-in-cs
        /// </summary>
        public static string GenerateSlug(string value, int? maxLength = null)
        {
            // prepare string, remove accents, lower case and convert hyphens to whitespace
            var result = RemoveAccent(value).Replace("-", " ").ToLowerInvariant();

            result = Regex.Replace(result, @"[^a-z0-9\s-]", string.Empty); // remove invalid characters
            result = Regex.Replace(result, @"\s+", " ").Trim(); // convert multiple spaces into one space

            if (maxLength.HasValue) // cut and trim
                result = result.Substring(0, result.Length <= maxLength ? result.Length : maxLength.Value).Trim();

            return Regex.Replace(result, @"\s", "-"); // replace all spaces with hyphens
        }

        /// <summary>
        /// Returns a string array containing the trimmed substrings in this <paramref name="value"/>
        /// that are delimited by the provided <paramref name="separators"/>.
        /// </summary>
        public static IEnumerable<string> SplitAndTrim(this string value, params char[] separators)
        {
            Ensure.Argument.NotNull(value, "source");

            return value.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
        }

        private static string RemoveAccent(string value)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// true, if is valid email address
        /// </summary>
        /// <param name="s">email address to test</param>
        /// <returns>true, if is valid email address</returns>
        public static bool IsValidEmailAddress(this string s)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(s);
        }

        /// <summary>
        /// Checks if url is valid. 
        /// and changed to match http://localhost
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidUrl(this string url)
        {
            string strRegex = "^(https?://)"
                            + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
                            + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
                            + "|" // allows either IP or domain
                            + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
                            + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level domain
                            + @"(\.[a-z]{2,6})?)" // first level domain- .com or .museum is optional
                            + "(:[0-9]{1,5})?" // port number- :80
                            + "((/?)|" // a slash isn't required if there is no file name
                            + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            return new Regex(strRegex).IsMatch(url);
        }

        /// <summary>
        /// Check if url (http) is available.
        /// </summary>
        /// <param name="httpUri">url to check</param>
        /// <example>
        /// string url = "www.codeproject.com;
        /// if( !url.UrlAvailable())
        ///     ...codeproject is not available
        /// </example>
        /// <returns>true if available</returns>
        public static bool UrlAvailable(this string httpUrl)
        {
            if (!httpUrl.StartsWith("http://") || !httpUrl.StartsWith("https://"))
                httpUrl = "http://" + httpUrl;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse myHttpWebResponse =
                   (HttpWebResponse)myRequest.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reduce string to shorter preview which is optionally ended by some string (...).
        /// </summary>
        /// <param name="s">string to reduce</param>
        /// <param name="count">Length of returned string including endings.</param>
        /// <param name="endings">optional edings of reduced text</param>
        /// <example>
        /// string description = "This is very long description of something";
        /// string preview = description.Reduce(20,"...");
        /// produce -> "This is very long..."
        /// </example>
        /// <returns></returns>
        public static string Reduce(this string s, int count, string endings)
        {
            if (count < endings.Length)
                throw new Exception("Failed to reduce to less then endings length.");
            int sLength = s.Length;
            int len = sLength;
            if (endings != null)
                len += endings.Length;
            if (count > sLength)
                return s; //it's too short to reduce
            s = s.Substring(0, sLength - len + count);
            if (endings != null)
                s += endings;
            return s;
        }

        /// <summary>
        /// remove white space, not line end
        /// Useful when parsing user input such phone,
        /// price int.Parse("1 000 000".RemoveSpaces(),.....
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value">string without spaces</param>
        public static string RemoveSpaces(this string s)
        {
            return s.Replace(" ", "");
        }

        /// <summary>
        /// true, if the string can be parse as Double respective Int32
        /// Spaces are not considred.
        /// </summary>
        /// <param name="s">input string</param>
        /// <param name="floatpoint">true, if Double is considered,
        /// otherwhise Int32 is considered.</param>
        /// <returns>true, if the string contains only digits or float-point</returns>
        public static bool IsNumber(this string s, bool floatpoint)
        {
            int i;
            double d;
            string withoutWhiteSpace = s.RemoveSpaces();
            if (floatpoint)
                return double.TryParse(withoutWhiteSpace, NumberStyles.Any,
                    Thread.CurrentThread.CurrentUICulture, out d);
            else
                return int.TryParse(withoutWhiteSpace, out i);
        }

        /// <summary>
        /// true, if the string contains only digits or float-point.
        /// Spaces are not considred.
        /// </summary>
        /// <param name="s">input string</param>
        /// <param name="floatpoint">true, if float-point is considered</param>
        /// <returns>true, if the string contains only digits or float-point</returns>
        public static bool IsNumberOnly(this string s, bool floatpoint)
        {
            s = s.Trim();
            if (s.Length == 0)
                return false;
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    if (floatpoint && (c == '.' || c == ','))
                        continue;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Remove accent from strings 
        /// </summary>
        /// <example>
        ///  input:  "Příliš žluťoučký kůň úpěl ďábelské ódy."
        ///  result: "Prilis zlutoucky kun upel dabelske ody."
        /// </example>
        /// <param name="s"></param>
        /// <returns>string without accents</returns>
        public static string RemoveDiacritics(this string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// Replace \r\n or \n by <br />
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Nl2Br(this string s)
        {
            return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MD5(this string s)
        {
            if (s_md5 == null) //creating only when needed
                s_md5 = new MD5CryptoServiceProvider();
            Byte[] newdata = Encoding.Default.GetBytes(s);
            Byte[] encrypted = s_md5.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this object obj)
        {
            return obj != null ? obj.ToString() : String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}