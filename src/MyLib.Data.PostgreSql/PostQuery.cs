using MyLib.Data.Common;
using System.Data;

namespace MyLib.Data.PostgreSql
{
	internal class PostQuery : QueryBase
	{
		public PostQuery(string sql) : base(sql)
		{
		}

		public PostQuery(
			string sql
			, IDbTransaction transaction
		) : base(sql, transaction)
		{
		}

		public PostQuery(
			string sql
			, ListFilter listFilter
		) : base(sql, listFilter)
		{
		}

		public PostQuery(
			string sql
			, ParameterListBase parameters
		) : base(sql, parameters)
		{
		}

		public PostQuery(
			string sql
			, string keyName
			, object keyValue
		) : base(sql, keyName, keyValue)
		{
		}

		public PostQuery(
			string sql
			, ParameterListBase parameters
			, IDbTransaction transaction
		) : base(sql, parameters, transaction)
		{
		}

		protected override ParameterListBase CreateParameterList()
		{
			return new PostParameterList();
		}
	}
}