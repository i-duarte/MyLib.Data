using System;
using System.Collections.Generic;

namespace MyLib.Extensions.Linq
{
	public static class Enumerable
    {
	    public static void ForEach<T>(
		    this IEnumerable<T> enumerable
		    , Action<T> action
	    )
	    {
		    foreach (var item in enumerable)
		    {
			    action(item);
		    }
	    }

	    public static void ForEachI<TSource>(
		    this IEnumerable<TSource> source
			, Action<TSource, int> action
		)
	    {
			var i = 0;
		    foreach (var item in source)
		    {
			    action(item, i);
			    i++;
		    }
		}

		public static void ForEach<T>(
			this IEnumerable<T> enumerable
			, Action<T, int> action
		)
		{
			var i = 0;
			foreach (var item in enumerable)
			{
				action(item, i);
				i++;
			}
		}

		public static string JoinWith(
			this IEnumerable<string> iEnum
			, string c
		)
		{
			return string.Join(c, iEnum);			
		}
	}
}
