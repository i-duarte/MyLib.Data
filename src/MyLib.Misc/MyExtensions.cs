using System;
using System.Collections.Generic;

namespace MyLib.Misc
{
	public static class MyExtensions
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
