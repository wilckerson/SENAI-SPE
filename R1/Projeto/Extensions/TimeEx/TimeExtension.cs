using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions.TimeEx
{
    public static class TimeExtension
    {
        public static DateTime Midnight(this DateTime current)
        {
            DateTime result = new DateTime(current.Year, current.Month, current.Day);
            return result;
        }

        public static DateTime Noon(this DateTime current)
        {
            DateTime result = new DateTime(current.Year, current.Month, current.Day, 12, 0, 0);
            return result;
        }

        #region Extension ToSetTime

        public static DateTime SetTime(this DateTime date, int hour)
        {
            return date.SetTime(hour, 0, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute)
        {
            return date.SetTime(hour, minute, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second)
        {
            return date.SetTime(hour, minute, second, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
        }

        #endregion
    }
}
