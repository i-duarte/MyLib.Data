using System;
using System.Collections.Generic;
using MyLib.Extensions.XLinq;
using System.Linq;
using System.Text;

namespace MyLib.Data.Common
{
	public static class MyExtensions
	{
		public static string SelectInsertParam(
			this IEnumerable<IField> fields
		) =>
			fields
			.JoinWithCommaSpace(
				f => $"@{f.Name}"
			);

		public static string SelectInsertField(
			this IEnumerable<IField> fields
		) =>
			fields
			.JoinWithCommaSpace(
				f => $"{f.Name}"
			);

		public static string SelectUpdate(
			this IEnumerable<IField> fields
		)
		{
			return
				fields
				.JoinWithCommaSpace(
					f => $"{f.Name} = @{f.Name}"
				);
		}

		public static IEnumerable<string> SelectEqualsParam(
			this IEnumerable<IField> fields

		) =>
			fields
			.Select(f => $"{f.Name} = @{f.Name}");

		public static string JoinWithAnd(
			this IEnumerable<IField> source
		) => source.SelectEqualsParam().JoinWithAnd();

		public static string JoinWithAnd(
			this IEnumerable<string> source
		) => source.JoinWith(" AND ");

		public static string JoinWithOr(
			this IEnumerable<IField> source
		) => source.SelectEqualsParam().JoinWithOr();

		public static string JoinWithOr(
			this IEnumerable<string> source
		) => source.JoinWith(" OR ");

		public static string JoinWithCommaSpace<TT>(
			this IEnumerable<TT> source
			, Func<TT, string> f
		) => source.JoinWith(f, ", ");
	}
}
