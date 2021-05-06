using MyLib.Extensions.XLinq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MyLib.Extensions.Data
{
	public static class Sql
	{
		public static object GetFirstValue(
			this SqlDataReader dr
		)
		{
			object v = null;
			if (dr.Read())
			{
				v = dr[0];
			}
			dr.Close();
			return v;
		}

		public static SqlDataReader ExecuteReader(
			this SqlCommand cmd
			, CommandBehavior commandBehavior
			, int timeOut
		)
		{
			cmd.CommandTimeout = timeOut;
			return cmd.ExecuteReader(commandBehavior);
		}

		public static int ExecuteNonQuery(
			this SqlCommand cmd
			, int timeOut
		)
		{
			cmd.CommandTimeout = timeOut;
			return cmd.ExecuteNonQuery();
		}

		public static int ExecuteNonQueryAndClose(
			this SqlCommand cmd
			, int timeOut = 30
		)
		{
			cmd.CommandTimeout = timeOut;
			var res = cmd.ExecuteNonQuery();
			if (cmd.Connection != null)
			{
				cmd.Connection.Close();
			}
			return res;
		}

		

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
