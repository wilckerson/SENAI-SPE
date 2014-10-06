using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Extensions.DateEx
{
    public static class DateExtension
    {
        /// <summary>
        /// Return a datetime with equal length string
        /// Eg.: 08/12/2050
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShortDateEqualLength(this DateTime dt)
        {
            var rv = new StringBuilder();
            if (dt.Month.ToString().Length ==1)
            {
                rv.Append("0");
            }
            rv.Append(dt.Month.ToString());
            rv.Append("/");
            if (dt.Day.ToString().Length == 1)
            {
                rv.Append("0");
            }
            rv.Append(dt.Day.ToString());
            rv.Append("/");
            rv.Append(dt.Year.ToString());

            return rv.ToString();
        }

        /// <summary>
        /// Return a first day of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Return a last day of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime AddWeekdays(this DateTime date, int days)
        {
            var sign = days < 0 ? -1 : 1;
            var unsignedDays = Math.Abs(days);
            var weekdaysAdded = 0;
            while (weekdaysAdded < unsignedDays)
            {
                date = date.AddDays(sign);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    weekdaysAdded++;
            }
            return date;
        }

        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int num = dayOfWeek - current.DayOfWeek;
            if (num <= 0)
            {
                num += 7;
            }
            return current.AddDays((double)num);
        }

        #region Extension ToRelativeDateString

        /// <summary>
        /// Return the relative date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToRelativeDateString(this DateTime date)
        {
            return GetRelativeDateValue(date, DateTime.Now);
        }

        /// <summary>
        /// Return the relative date based on datetime UTC
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToRelativeDateStringUtc(this DateTime date)
        {
            return GetRelativeDateValue(date, DateTime.UtcNow);
        }

        #endregion

        #region Extension ToString()

        public static string ToString(this DateTime? date)
        {
            return date.ToString(null, DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToString(this DateTime? date, string format)
        {
            return date.ToString(format, DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToString(this DateTime? date, IFormatProvider provider)
        {
            return date.ToString(null, provider);
        }

        public static string ToString(this DateTime? date, string format, IFormatProvider provider)
        {
            if (date.HasValue)
                return date.Value.ToString(format, provider);
            else
                return string.Empty;
        }

        #endregion
        
        #region Auxiliar Methods

        /// <summary>
        /// Verify when ocurr the event in days, hours or minutes comparation
        /// </summary>
        /// <param name="date"></param>
        /// <param name="comparedTo"></param>
        /// <returns></returns>
        private static string GetRelativeDateValue(DateTime date, DateTime comparedTo)
        {
            TimeSpan diff = comparedTo.Subtract(date);
            if (diff.TotalDays >= 365)
                return string.Concat("on ", date.ToString("MMMM d, yyyy"));
            if (diff.TotalDays >= 7)
                return string.Concat("on ", date.ToString("MMMM d"));
            else if (diff.TotalDays > 1)
                return string.Format("{0:N0} days ago", diff.TotalDays);
            else if (diff.TotalDays == 1)
                return "yesterday";
            else if (diff.TotalHours >= 2)
                return string.Format("{0:N0} hours ago", diff.TotalHours);
            else if (diff.TotalMinutes >= 60)
                return "more than an hour ago";
            else if (diff.TotalMinutes >= 5)
                return string.Format("{0:N0} minutes ago", diff.TotalMinutes);
            if (diff.TotalMinutes >= 1)
                return "a few minutes ago";
            else
                return "less than a minute ago";
        }

        #endregion
    }
}