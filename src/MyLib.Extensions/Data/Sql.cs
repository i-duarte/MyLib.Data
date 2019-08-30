using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyLib.Extensions.Data
{
	public static class Sql
	{
		public static SqlBulkCopy MapColumns(
			this SqlBulkCopy bc
			, int numColumns
		)
		{
			for (var i = 0; i < numColumns; i++)
			{
				bc.ColumnMappings.Add(i, i);
			}
			return bc;
		}

		public static SqlBulkCopy SetTimeOut(
			this SqlBulkCopy bc
			, int timeOut
		)
		{
			bc.BulkCopyTimeout = timeOut;
			return bc;
		}

		public static object GetFirstValue(
			this SqlDataReader dr
			, bool close = true
		)
		{
			object v = null;

			if (dr.Read())
			{
				v = dr[0];
			}

			if (close)
			{
				dr.Close();
			}

			return v;
		}

		public static SqlCommand SetTimeOut(
			this SqlCommand cmd
			, int timeOut
		)
		{
			cmd.CommandTimeout = timeOut;
			return cmd;
		}

		public static SqlCommand AddParameters<T>(
			this SqlCommand cmd
			, List<T> parameterList
		)
		{
			if (parameterList != null)
			{
				cmd.Parameters.AddRange(parameterList.ToArray());
			}
			return cmd;
		}

		public static SqlDataReader ExecuteReader(
			this SqlCommand cmd
			, int timeOut
		)
		{
			cmd.CommandTimeout = timeOut;
			return cmd.ExecuteReader();
		}

		public static SqlDataReader ExecuteReader(
			this SqlCommand cmd
			, CommandBehavior commnadBehavior
			, int timeOut 
		)
		{
			cmd.CommandTimeout = timeOut;
			return cmd.ExecuteReader(commnadBehavior);
		}

		public static int ExecuteNonQueryAndClose(
			this SqlCommand cmd
			, int timeOut = 30
		)
		{
			cmd.CommandTimeout = timeOut;
			var i = cmd.ExecuteNonQuery();
			cmd.Connection.Close();
			cmd.Dispose();
			return i;
		}

		public static int ExecuteNonQuery(
			this SqlCommand cmd
			, int timeOut = 30
		)
		{
			cmd.CommandTimeout = timeOut;
			return cmd.ExecuteNonQuery();
		}
	}
}
