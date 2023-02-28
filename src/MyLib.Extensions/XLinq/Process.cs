﻿using System;
using System.Collections.Generic;

namespace MyLib.Extensions.XLinq
{
    public static class Process
    {
        public static T Pipe<T>(
            this T source
            , Action<T> a
        )
        {
            a(source);
            return source;
        }

        public static TR Pipe<T, TR>(
            this T x
            , Func<T, TR> f
        )
            => f(x);

        public static T PipeIf<T>(
            this T x
            , bool exp
            , Func<T, T> f
        )
            => exp
                ? f(x)
                : x;

        public static T PipeIf<T>(
            this T x
            , bool exp
            , Func<T, T> f
            , Func<T, T> f2
        )
            => exp
                ? f(x)
                : f2(x);

        public static void Do<T>(this T x, Action<T> a) => a(x);

        public static List<T> WrapToList<T>(this T x) => new List<T> { x };



    }
}
