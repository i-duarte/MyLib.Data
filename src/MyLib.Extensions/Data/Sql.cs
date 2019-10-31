using MyLib.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace MyLib.Extensions.Data
{
	public static class Sql
	{
		public static DataTable ToDataTable<T>(
			this IEnumerable<T> source
		)
		{
			var properties =
				GetProperties(typeof(T))
				.ToList();

			var table = CreateTable(properties);				
			
			source
			.ForEach(
				item =>
				{
					table
					.AddRow(
						item
						, properties
					);
				}
			);
						
			return table;
		}

		private static DataTable AddRow<T>(
			this DataTable table
			, T item
			, List<PropertyDescriptor> properties
		)
		{
			var row = table.NewRow();
			properties.ForEach(
				prop =>
				row[prop.Name] =
					prop.GetValue(item)
						?? DBNull.Value
			);
			return table;
		}

		private static DataTable CreateTable(
			List<PropertyDescriptor> properties
		)
		{
			var table = new DataTable();
 
			table
			.Columns
			.AddRange(
				properties
				.Select(GetColumn)
				.ToArray()
			);

			return table;
		}

		private static DataColumn GetColumn(
			PropertyDescriptor prop
		) => 
			new DataColumn(
				prop.Name
				, Nullable
					.GetUnderlyingType(prop.PropertyType)
					?? prop.PropertyType
			);

		private static IEnumerable<PropertyDescriptor> GetProperties(
			Type t
		)
		{
			foreach(
				PropertyDescriptor item 
					in TypeDescriptor.GetProperties(t)
			)
			{
				yield return item;
			}
		}
	}
}
