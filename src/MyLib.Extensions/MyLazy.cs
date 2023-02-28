using System;

namespace MyLib.Extensions
{
    public static class MyLazy
    {
        public static T GetLazy<T>(
            ref T lazy
            , T o
        ) => lazy == null ? (lazy = o) : lazy;

        public static T GetLazy<T>(
            ref T lazy
            , Func<T> f
        ) => lazy == null ? (lazy = f()) : lazy;

        public static T GetLazy<T>(
            ref T lazy
            , Func<T> f
            , T emptyValue
        ) where T : IComparable
        {
            if (lazy.CompareTo(emptyValue) == 0)
            {
                return lazy = f();
            }
            return lazy;
        }
    }
}
