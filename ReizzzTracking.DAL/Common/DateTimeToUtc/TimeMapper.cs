using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Common.DateTimeToUtc
{
    public static class TimeMapper
    {
        public static TimeOnly FromTimeStringUtc7ToUtc(string utc7timeString)
        {

            TimeOnly utc7Time = TimeOnly.ParseExact(utc7timeString, "HH:mm");
            int utcHour = utc7Time.Hour - 7;
            if (utcHour < 0)
            {
                utcHour = 24 - utc7Time.Hour;
            }
            return new TimeOnly(utcHour, utc7Time.Minute);
        }
        public static DateTimeOffset GetDateTimeOffSetFromTimeOnly(TimeOnly time)
        {
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            return new DateTimeOffset(new DateOnly(utcNow.Year, utcNow.Month, utcNow.Day), time, TimeSpan.Zero);
        }
        public static DateTime FromUtc7ToUtc(DateTime utc7DateTime)
        {
            return utc7DateTime.AddHours(-7);
        }
    }
}
