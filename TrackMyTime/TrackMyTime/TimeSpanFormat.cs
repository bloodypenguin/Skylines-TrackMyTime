using System;
using System.Text;

namespace TrackMyTime
{
    public static class TimeSpanFormat
    {
        public static string ToString(TimeSpan value, bool milliseconds = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num1 = (int)(value.Ticks / 864000000000L);
            long num2 = value.Ticks % 864000000000L;
            if (value.Ticks < 0L)
            {
                stringBuilder.Append("-");
                num1 = -num1;
                num2 = -num2;
            }
            stringBuilder.Append(num1 + "d");
            stringBuilder.Append(", ");
            stringBuilder.Append(IntToString((int)(num2 / 36000000000L % 24L), 2) + " h");
            stringBuilder.Append(", ");
            stringBuilder.Append(IntToString((int)(num2 / 600000000L % 60L), 2) + " m");
            stringBuilder.Append(", ");
            stringBuilder.Append(IntToString((int)(num2 / 10000000L % 60L), 2) + " s");
            if (milliseconds)
            {
                int n = (int)(num2 % 10000000L);
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