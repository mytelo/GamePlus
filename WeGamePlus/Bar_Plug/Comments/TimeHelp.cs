using System;

namespace WeGamePlus.Bar_Plug.Comments
{
    public class TimeHelp
    {
        public static long ConvertDateTimeToInt(DateTime time)
        {
            DateTime time2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0, 0));
            return ((time.Ticks - time2.Ticks) / 0x2710L);
        }

        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            long ticks = long.Parse(timeStamp + "0000");
            TimeSpan span = new TimeSpan(ticks);
            return time.Add(span);
        }

        public static DateTime GetDateTimeFrom1970Ticks(long curSeconds) => 
            TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1)).AddSeconds((double) curSeconds);

        public static string GetTimeStamp(DateTime time, int length = 13) => 
            ConvertDateTimeToInt(time).ToString().Substring(0, length);

        public static bool IsTime(string time) => 
            GetTimeStamp(DateTime.Now, 8).Equals(time);

        public static bool IsTime(long time, double interval)
        {
            DateTime time2 = GetDateTimeFrom1970Ticks(time);
            DateTime time3 = DateTime.Now.AddMinutes(interval);
            DateTime time4 = DateTime.Now.AddMinutes(interval * -1.0);
            return ((time2 > time4) && (time2 < time3));
        }
    }
}

