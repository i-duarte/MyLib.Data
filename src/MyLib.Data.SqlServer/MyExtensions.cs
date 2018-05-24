using System.Data.SqlClient;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
	internal static class MyExtensions
	{
		public static SqlBulkCopy MapColumns(
			this SqlBulkCopy bc
			, int numColumns
		)
		{
			for(var i = 0; i < numColumns; i++)
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

		public static SqlCommand SetTimeOut(
			this SqlCommand cmd
			, int timeOut
		)
		{
			cmd.CommandTimeout = timeOut;
			return cmd;
		}

		public static SqlCommand AddParameters(
			this SqlCommand cmd
			, ParameterListBase parameterList
		)
		{
			cmd.Parameters.AddRange(parameterList.ToArray());
			return cmd;
		}
	}
}
