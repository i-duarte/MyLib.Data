using System;
using System.Collections.Generic;
using MyLib.Extensions.Linq;
using System.Linq;

namespace MyLib.Data.EntityFramework
{
	public static class MyExtensions
	{
		public static string SelectInsertParam(
			this IEnumerable<PropertyField> fields
		) => 
			fields
			.JoinWithCommaSpace(
				f => $"@{f.Name}"
			);

		public static string SelectInsertField(
			this IEnumerable<PropertyField> fields
		) => 
			fields
			.JoinWithCommaSpace(
				f => $"{f.Name}"
			);

		public static string SelectUpdate(
			this IEnumerable<PropertyField> fields
		)
		{
			return
				fields
				.JoinWithCommaSpace(
					f => $"{f.Name} = @{f.Name}"
				);
		}

		public static IEnumerable<string> SelectEqualsParam(
			this IEnumerable<PropertyField> fields

		) => 
			fields
			.Select(f => $"{f.Name} = @{f.Name}");

		public static string JoinWithAnd(
			this IEnumerable<PropertyField> source
		) => source.SelectEqualsParam().JoinWith(" AND ");

		public static string JoinWithOr(
			this IEnumerable<PropertyField> source
		) => source.SelectEqualsParam().JoinWith(" OR ");

		public static string JoinWithCommaSpace<TT>(
			this IEnumerable<TT> source
			, Func<TT, string> f
		) => source.JoinWith(f, ", ");
	}
}
