using System.Data;

namespace MyLib.Data.Common
{
    public abstract class QueryAdapterBase 
    {
		public abstract ParameterListBase CreateParameterList();
		public abstract ParameterListBase CreateParameterList<T>(string name, T t);

		public abstract IDataReader GetDataReader(
			QueryBase query
		);

	    public abstract int Execute(
		    QueryBase query
	    );

		public abstract T Get<T>(
			QueryBase query
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
	}
}
