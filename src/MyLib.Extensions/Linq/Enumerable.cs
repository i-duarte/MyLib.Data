using System;
using System.Collections.Generic;

namespace MyLib.Extensions.Linq
{
	public static class Enumerable
    {
		public static void Foreach<T>(
				this IEnumerable<T> enumerable
				, Action<T> action
			)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}

		public static string Concat(
			this IEnumerable<string> iEnum
			, string c
		)
		{
			return string.Join(c, iEnum);			
		}
	}
}
