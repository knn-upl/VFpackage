using System;
using System.Globalization;

namespace VF
{    
    public static class DateTimeExtensions
    {
        public static TimeZoneInfo DefaultUTCTimeZone { get; set; } = TimeZoneInfo.Utc;
        public static TimeZoneInfo DefaultTimeZone { get; set; } = TimeZoneInfo.Local;
        public static string DefaultCulture { get; set; } = "en-EN";
        public static string DefaultCultureFormat { get; set; } = "dd MMMM yyyy";
            
        public static DateTime ToDefaultTimeZone(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, DefaultTimeZone);
        }

        public static DateTime ToUtc(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, DefaultUTCTimeZone);
        }

        // Get the current time with the default timezone
        public static DateTime GetCurrentTime()
        {
            return DateTime.Now.ToDefaultTimeZone();
        }
        public static DateTime LastDateOfMonth => new(GetCurrentTime().Year, GetCurrentTime().Month, DateTime.DaysInMonth(GetCurrentTime().Year, GetCurrentTime().Month), 23, 59, 59);
        public static DateTime FirstDateOfMonth => new(GetCurrentTime().Year, GetCurrentTime().Month, 1, 0, 0, 0);



        private static readonly CultureInfo culture = CultureInfo.CreateSpecificCulture(DefaultCulture);
        public static string CultureFormat(DateTime dateTime)
        {
            return dateTime.ToString(DefaultCultureFormat, culture);
        }
    }

}
