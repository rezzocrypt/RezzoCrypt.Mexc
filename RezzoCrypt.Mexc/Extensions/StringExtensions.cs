using System;

namespace RezzoCrypt.Mexc.Extensions
{
    public static class StringExtensions
    {
        public static DateTime DateFromTicks(this string tick)
        {
            if (tick == null)
                return DateTime.MinValue;
            return long.Parse(tick).DateFromTicks();
        }

        public static DateTime DateFromTicks(this long tick)
        {
            var ticks = (tick + 62135596800000L) * 10000L;
            return new DateTime(ticks, DateTimeKind.Local);
        }

        public static long TicksFromDate(this DateTime dateTime)
        {
            return dateTime.Ticks / 10000L - 62135596800000L;
        }

        public static string StringTicksFromDate(this DateTime dateTime)
        {
            return dateTime.TicksFromDate().ToString();
        }
    }
}
