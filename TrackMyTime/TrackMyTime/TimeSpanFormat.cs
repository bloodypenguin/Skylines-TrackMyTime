using System;
using System.ComponentModel;
using System.Text;

namespace TrackMyTime
{
    public static class TimeSpanFormat
    {
        public static string ToString(TimeSpan value, ExternalLogFormat externalLogFormat)
        {
            StringBuilder stringBuilder = new StringBuilder();
            long finalTicks;
            if (externalLogFormat == ExternalLogFormat.DayHourMinute)
            {
                int days = (int) (value.Ticks / 864000000000L);
                long ticksWithoutDays = value.Ticks % 864000000000L;
                if (value.Ticks < 0L)
                {
                    stringBuilder.Append("-");
                    days = -days;
                    ticksWithoutDays = -ticksWithoutDays;
                }
                stringBuilder.Append(days + "d:");
                stringBuilder.Append(IntToString((int)(ticksWithoutDays / 36000000000L % 24L), 2) + "h:");
                finalTicks = ticksWithoutDays;
            }
            else
            {
                if (value.Ticks < 0L)
                {
                    stringBuilder.Append("-");
                    finalTicks = -value.Ticks;
                }
                else
                {
                    finalTicks = value.Ticks;
                }
                stringBuilder.Append((int)(finalTicks / 36000000000L) + "h:");
            }
            stringBuilder.Append(IntToString((int) (finalTicks / 600000000L % 60L), 2) + "m");
            return stringBuilder.ToString();
        }


        public static string ToString(TimeSpan value, bool milliseconds = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int days = (int) (value.Ticks / 864000000000L);
            long ticksWithoutDays = value.Ticks % 864000000000L;
            if (value.Ticks < 0L)
            {
                stringBuilder.Append("-");
                days = -days;
                ticksWithoutDays = -ticksWithoutDays;
            }
            stringBuilder.Append(days + "d");
            stringBuilder.Append(", ");
            stringBuilder.Append(IntToString((int) (ticksWithoutDays / 36000000000L % 24L), 2) + " h, ");
            stringBuilder.Append(IntToString((int) (ticksWithoutDays / 600000000L % 60L), 2) + " m, ");
            stringBuilder.Append(IntToString((int) (ticksWithoutDays / 10000000L % 60L), 2) + " s");
            if (milliseconds)
            {
                int n = (int) (ticksWithoutDays % 10000000L);
                if (n != 0)
                {
                    stringBuilder.Append(", ");
                    stringBuilder.Append(IntToString(n, 7) + " ms");
                }
            }
            return stringBuilder.ToString();
        }

        private static string IntToString(int n, int digits)
        {
            return n.ToString($"D{digits}");
        }
    }
}