using System;
using System.Linq;

namespace MyLib.Extensions
{
    public static class MyComparable
    {
        public static bool Between<T>(this T source, T from, T to)
            where T : IComparable
        => !(source.CompareTo(from) < 0 || source.CompareTo(to) > 0);

        public static bool In<T>(this T source, params T[] list)
            where T : IComparable
        => list.Any(i => source.Equals(i));
    }
}
