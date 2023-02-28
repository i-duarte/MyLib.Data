using System;

namespace MyLib.Extensions
{
    public static class MyDate
    {
        public static int ToSerialDate(
            this DateTime f
        ) =>
            Convert.ToInt32($"{f:yyyyMMdd}");

        public static long ToSerialDateTime(
            this DateTime f
        ) =>
            Convert.ToInt64($"{f:yyyyMMddHHmmss}");
    }
}
