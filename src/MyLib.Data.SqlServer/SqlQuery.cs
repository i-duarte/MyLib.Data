 using System.Data;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
	public class SqlQuery : QueryBase
	{
		public SqlQuery() { }

		public SqlQuery(string sql) : base(sql)
		{
		}

		public SqlQuery(
			string sql
			, ParameterListBase parameters
		) 
			: base(sql, parameters)
		{
		}
		
		public SqlQuery(
			string sql
			, string keyName
			, object keyValue
		) 
			: base(sql, keyName, keyValue)
		{
		}

		public SqlQuery(
			string sql
			, ListFilter listFilter
		) 
			: base(sql, listFilter)
		{
		}

		public SqlQuery(
			string sql
			, IDbTransaction transaction
		) 
			: base(sql, transaction)
		{
		}

		public SqlQuery(
			string sql
			, ParameterListBase parameters
			, IDbTransaction transaction
		) 
			: base(sql, parameters, transaction)
		{
		}

		protected override ParameterListBase CreateParameterList()
		{
			return new SqlParameterList();
		}
	}
}
