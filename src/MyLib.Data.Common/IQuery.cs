using System.Data;

namespace MyLib.Data.Common
{
    public interface IQuery
    {
	    IDataReader GetDataReader(
			string sql
		);

		IDataReader GetDataReader(
			string sql
			, IParameterList parameterList
		);

	    IDataReader GetDataReader(
		    string sql
		    , IDbConnection connection
	    );

	    IDataReader GetDataReader(
		    string sql
		    , IParameterList parameterList
		    , IDbConnection connection
	    );

	    IDataReader GetDataReader(
		    string sql
		    , IDbTransaction transaction
	    );

		IDataReader GetDataReader(
			string sql
			, IParameterList parameterList
			, IDbTransaction transaction
		);

	    IDataReader GetDataReader(
		    string sql
		    , IParameterList parameterList
		    , IDbTransaction transaction
			, int timeOut
	    );

		int Execute(
		    string sql
	    );

	    int Execute(
		    string sql
		    , IParameterList parameterList
	    );

	    int Execute(
		    string sql
		    , IDbConnection connection
	    );

	    int Execute(
		    string sql
		    , IParameterList parameterList
		    , IDbConnection connection
	    );
			
	    int Execute(
		    string sql
		    , IDbTransaction transaction
	    );

	    int Execute(
		    string sql
		    , IParameterList parameterList
		    , IDbTransaction transaction
	    );

	    void BulkCopy(
		    IDataReader reader
			, string table
			, int numFields
			, bool deleteRecords = true
	    );

	    void BulkCopy(
		    IDataReader reader 
			, string table 
			, int numFields 
			, IDbTransaction trans 
			, bool deleteRecords = true
	    );

	    void BulkCopy(
		    DataTable dt 
			, string table
			, int numFields
			, bool deleteRecords = true
		);

	    T Get<T>(
			string sql
		);

	    T Get<T>(
		    string sql
		    , IParameterList parameterList
	    );

	    T Get<T>(
		    string sql
		    , IDbConnection connection
	    );

	    T Get<T>(
		    string sql
		    , IParameterList parameterList
			, IDbConnection connection
	    );

		T Get<T>(
		    string sql
		    , IDbTransaction transaction
	    );

	    T Get<T>(
		    string sql
		    , IParameterList parameterList
			, IDbTransaction transaction
	    );

	}
}
