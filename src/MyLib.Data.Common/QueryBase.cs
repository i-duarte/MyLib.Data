using System.Data;

namespace MyLib.Data.Common
{
	public abstract class QueryBase
	{
		public string Sql { get; set; }

		private ParameterListBase _paramterList;
		public ParameterListBase Parameters
			=> _paramterList
			?? (_paramterList = CreateParameterList());

		public IDbTransaction Transaction { get; set; }
		public IDbConnection Connection { get; set; }

		public int TimeOut { get; set; } = 30;

		protected abstract ParameterListBase CreateParameterList();

		public QueryBase() { }

		public QueryBase(string sql)
		{
			Sql = sql;
		}

		public QueryBase(string sql, ParameterListBase parameters)
		{
			Sql = sql;
			_paramterList = parameters;
		}

		public QueryBase(string sql, ParameterListBase parameters, IDbTransaction transaction)
		{
			Sql = sql;
			_paramterList = parameters;
			Transaction = transaction;
		}

		public QueryBase(string sql, IDbTransaction transaction)
		{
			Sql = sql;
			Transaction = transaction;
		}
	}
}
