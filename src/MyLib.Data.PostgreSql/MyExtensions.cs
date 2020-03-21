using Npgsql;
using System.Collections.Generic;

namespace MyLib.Data.PostgreSql
{
	public static class MyExtensions
	{
		public static NpgsqlCommand AddParameters<T>(
			this NpgsqlCommand cmd
			, List<T> parameterList
		)
		{
			if (parameterList != null)
			{
				cmd.Parameters.AddRange(parameterList.ToArray());
			}
			return cmd;
		}

	}
}
