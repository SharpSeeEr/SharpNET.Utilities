using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNET.Utilities.NETStandard
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Implementation of ToShortDateString() for Nullable DateTime
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTime? source)
        {
            if (source.HasValue) return source.Value.ToString("d");
            return null;
        }

        public static DateTime ToTimezone(this DateTime source, string timezoneId)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            return ToTimezone(source, timezoneInfo);
        }

        public static DateTime ToTimezone(this DateTime source, TimeZoneInfo timezoneInfo)
        {
            return TimeZoneInfo.ConvertTime(source, TimeZoneInfo.Utc, timezoneInfo);
        }

        public static DateTime? ToTimezone(this DateTime? source, string timezoneId)
        {
            if (!source.HasValue) return source;
            return source.Value.ToTimezone(timezoneId);
        }

        public static DateTime? ToTimezone(this DateTime? source, TimeZoneInfo timezoneInfo)
        {
            if (!source.HasValue) return source;
            return source.Value.ToTimezone(timezoneInfo);
        }
    }
}
