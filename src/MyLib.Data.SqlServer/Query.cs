using System.Data;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
    public class Query : IQuery
    {
		protected ConnectionFactory ConnectionFactory { get; set; }

	    public Query(ConnectionFactory connectionFactory)
	    {
		    ConnectionFactory = connectionFactory;
	    }

	    public IDataReader GetDataReader(string sql)
	    {
		    throw new System.NotImplementedException();
	    }

	    public IDataReader GetDataReader(string sql, IParameterList parameterList)
	    {
		    throw new System.NotImplementedException();
	    }

	    public IDataReader GetDataReader(string sql, IDbConnection connection)
	    {
		    throw new System.NotImplementedException();
	    }

	    public IDataReader GetDataReader(string sql, IParameterList parameterList, IDbConnection connection)
	    {
		    throw new System.NotImplementedException();
	    }

	    public IDataReader GetDataReader(string sql, IDbTransaction transaction)
	    {
		    throw new System.NotImplementedException();
	    }

	    public IDataReader GetDataReader(string sql, IParameterList parameterList, IDbTransaction transaction)
	    {
		    throw new System.NotImplementedException();
	    }

	    public int Execute(string sql)
	    {
		    throw new System.NotImplementedException();
	    }

	    public int Execute(string sql, IParameterList parameterList)
	    {
		    throw new System.NotImplementedException();
	    }

	    public int Execute(string sql, IDbConnection connection)
	    {
		    throw new System.NotImplementedException();
	    }

	    public int Execute(string sql, IParameterList parameterList, IDbConnection connection)
	    {
		    throw new System.NotImplementedException();
	    }

	    public int Execute(string sql, IDbTransaction transaction)
	    {
		    throw new System.NotImplementedException();
	    }

	    public int Execute(string sql, IParameterList parameterList, IDbTransaction transaction)
	    {
		    throw new System.NotImplementedException();
	    }

	    public void BulkCopy(IDataReader reader, string table, int numFields, bool deleteRecords = true)
	    {
		    throw new System.NotImplementedException();
	    }

	    public void BulkCopy(IDataReader reader, string table, int numFields, IDbTransaction trans, bool deleteRecords = true)
	    {
		    throw new System.NotImplementedException();
	    }

	    public void BulkCopy(DataTable dt, string table, int numFields)
	    {
		    throw new System.NotImplementedException();
	    }

	    public T Get<T>(string sql)
	    {
		    throw new System.NotImplementedException();
	    }

	    public T Get<T>(string sql, IParameterList parameterList)
	    {
		    throw new System.NotImplementedException();
	    }

	    public T Get<T>(string sql, IDbConnection connection)
	    {
		    throw new System.NotImplementedException();
	    }

	    public T Get<T>(string sql, IParameterList parameterList, IDbConnection connection)
	    {
		    throw new System.NotImplementedException();
	    }

	    public T Get<T>(string sql, IDbTransaction transaction)
	    {
		    throw new System.NotImplementedException();
	    }

	    public T Get<T>(string sql, IParameterList parameterList, IDbTransaction transaction)
	    {
		    throw new System.NotImplementedException();
	    }
    }
}
