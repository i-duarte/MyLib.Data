using System;
using System.Collections.Generic;

namespace MyLib.Extensions.Linq
{
	public static class IEnumerable
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
	}
}
