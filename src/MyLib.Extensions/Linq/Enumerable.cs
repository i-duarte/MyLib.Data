using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLib.Extensions.Linq
{
	public static class Enumerable
    {
		public static IEnumerable<T> ToEnumerable<T>(
			this ICollection source
		)
		{
			foreach (var i in source)
			{
				yield return (T)i;
			}
		}

		public static IEnumerable<T> AddRowNumbers<T>(
			this IEnumerable<T> source
		) where T : INumbered => 
			source.Select(
				(r, i) =>
				{
					r.Number = i + 1;
					return r;
				}
			);

		public static IEnumerable<TSource> OrderBy<TSource, TKey>(
			this IEnumerable<TSource> source
			, Func<TSource, TKey> keySelector
			, bool asc
		) => 
			asc
				? source.OrderBy(keySelector)
				: source.OrderByDescending(keySelector);

		public static bool AreEquals<T>(
			this IEnumerable<T> source
			, IEnumerable<T> other
		)
		{
			var sourceEnum =
				source.GetEnumerator();
			var otherEnum =
				other.GetEnumerator();

			while (true)
			{
				if (sourceEnum.MoveNext())
				{
					if (!otherEnum.MoveNext())
					{
						return false;
					}
				}
				else
				{
					if (otherEnum.MoveNext())
					{
						return false;
					}
					else
					{
						break;
					}
				}

				if (
					!EqualsProperties(
						sourceEnum.Current
						, otherEnum.Current
					)
				)
				{
					return false;
				}
			}

			return !sourceEnum.MoveNext() && !otherEnum.MoveNext();
		}

		private static bool EqualsProperties<T>(T arg1, T arg2) 
		{
			if (
				arg1 == null
				^ arg2 == null
			)
			{
				return false;
			}

			if (
				arg1 != null &&
				!arg1.Equals(arg2)
			)
			{
				return false;
			}
			return true;
		}

		public static IEnumerable<object> SelectProperty<T>(
			this IEnumerable<T> source
			, string propertyName
		) => source.Select(t => GetPropValue(t, propertyName));

		private static object GetPropValue(
			object src
			, string propName
		) => 
			src
			.GetType()
			.GetProperty(propName)
			.GetValue(src, null)
			;

		public static IEnumerable<T> SelectIf<T>(
			this IEnumerable<T> source
			, Func<T, bool> funExp
			, Func<T, T> fun
		)
		{
			foreach (var item in source)
			{
				yield return 
					funExp(item) 
					? fun(item) 
					: item;
			}
		}


		public static IEnumerable<T> SelectIgnore<T>(
			this IEnumerable<T> source
			, int count
			, Func<T, T> fun
		)
		{
			var i = 1;
			foreach (var item in source)
			{
				yield return
					i > count 
					? fun(item) 
					: item;
				i++;
			}
		}

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

	 //   public static void ForEachI<TSource>(
		//    this IEnumerable<TSource> source
		//	, Action<TSource, int> action
		//)
	 //   {
		//	var i = 0;
		//    foreach (var item in source)
		//    {
		//	    action(item, i);
		//	    i++;
		//    }
		//}

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

		public static IEnumerable<T> Prepend<T>(
			this IEnumerable<T> source
			, T item
		)
		{
			yield return item;
			foreach(var i in source)
			{
				yield return i;
			}
		}

		public static string JoinWith<T>(
			this IEnumerable<T> source
			, Func<T, string> f
			, string separator
		) =>
			string.Join(
				separator
				, source
					.Select(f)
					.JoinWith(separator)
			);

		public static string JoinWith<T>(
			this IEnumerable<T> source
			, Func<T, string> f
			, string separator
			, string quotation
		) =>
			string.Join(
				separator
				, source
					.Select(f)
					.JoinWith(separator, quotation)
			);

		public static string JoinWith<T>(
			this IEnumerable<T> iEnum
			, string separator
		) => string.Join(separator, iEnum.Select(i => $"{i}"));

		public static string JoinWith<T>(
			this IEnumerable<T> iEnum
			, string separator
			, string quotation
		) => 
			$"{quotation}" +
			$"{iEnum.JoinWith(separator)}" +
			$"{quotation}";
	}
}
