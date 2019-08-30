using System.Collections.Generic;

namespace MyLib.Extensions.Linq
{
	public static class List
	{
		public static void Replace<T>(
			this List<T> source
			, IEnumerable<T> newList
		)
		{
			source.Clear();
			source.AddRange(newList);
		}

		public static void Replace<T>(
			this List<T> source
			, List<T> newList
		)
		{
			source.Clear();
			source.AddRange(newList);
		}
	}
}
