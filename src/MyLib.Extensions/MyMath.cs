using System;
using System.Linq;

namespace MyLib.Extensions
{
    public static class MyConvert
    {
        public static DateTime ToDateTime(this string s)
            => DateTime.Parse(s);

        public static int ToInt(this string s)
            => int.Parse(s);

        public static decimal ToDecimal(this string s)
            => decimal.Parse(s);

        public static decimal? ToDecimalNull(this string s)
            => string.IsNullOrWhiteSpace(s)
                ? (decimal?)null
                : decimal.Parse(s);
    }

    public static class MyMath
    {
        public static decimal Max(
            params decimal[] values
        )
        {
            return values.Max();
        }

        public static decimal Round(this decimal v, int n)
            => Math.Round(v, n);
    }
}
