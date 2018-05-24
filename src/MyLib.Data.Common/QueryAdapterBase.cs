using System.Collections.Generic;
using System.Data;

namespace MyLib.Data.Common
{
    public abstract class QueryAdapterBase : List<IDbDataParameter>
    {
	    public abstract IDataReader GetDataReader(
			string sql
		);

	    public abstract IDataReader GetDataReader(
			string sql
			, ParameterListBase parameterList
		);

	    public abstract IDataReader GetDataReader(
		    string sql
		    , IDbConnection connection
	    );

	    public abstract IDataReader GetDataReader(
		    string sql
		    , ParameterListBase parameterList
		    , IDbConnection connection
	    );

	    public abstract IDataReader GetDataReader(
		    string sql
		    , IDbTransaction transaction
	    );

	    public abstract IDataReader GetDataReader(
			string sql
			, ParameterListBase parameterList
			, IDbTransaction transaction
		);

	    public abstract IDataReader GetDataReader(
		    string sql
		    , ParameterListBase parameterList
		    , IDbTransaction transaction
			, int timeOut
	    );

	    public abstract int Execute(
		    string sql
	    );

	    public abstract int Execute(
		    string sql
		    , ParameterListBase parameterList
	    );

	    public abstract int Execute(
		    string sql
		    , IDbConnection connection
	    );

	    public abstract int Execute(
		    string sql
		    , ParameterListBase parameterList
		    , IDbConnection connection
	    );

	    public abstract int Execute(
		    string sql
		    , IDbTransaction transaction
	    );

	    public abstract int Execute(
		    string sql
		    , ParameterListBase parameterList
		    , IDbTransaction transaction
	    );

	    public abstract void BulkCopy(
		    IDataReader reader
			, string table
			, int numFields
			, bool deleteRecords = true
	    );

	    public abstract void BulkCopy(
		    IDataReader reader 
			, string table 
			, int numFields 
			, IDbTransaction trans 
			, bool deleteRecords = true
	    );

	    public abstract void BulkCopy(
		    DataTable dt 
			, string table
			, int numFields
			, bool deleteRecords = true
		);

	    public abstract T Get<T>(
			string sql
		);

	    public abstract T Get<T>(
		    string sql
		    , ParameterListBase parameterList
	    );

	    public abstract T Get<T>(
		    string sql
		    , IDbConnection connection
	    );

	    public abstract T Get<T>(
		    string sql
		    , ParameterListBase parameterList
			, IDbConnection connection
	    );

	    public abstract T Get<T>(
		    string sql
		    , IDbTransaction transaction
	    );

	    public abstract T Get<T>(
		    string sql
		    , ParameterListBase parameterList
			, IDbTransaction transaction
	    );

	}
}
