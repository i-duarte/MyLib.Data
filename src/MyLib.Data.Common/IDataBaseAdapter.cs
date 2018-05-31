using System.Data;

namespace MyLib.Data.Common
{
	public interface IDataBaseAdapter
	{
		IDbConnection GetConnection();
		IDbConnection GetConnection(int timeOut);
		IDbTransaction GetTransaction();
		IDbTransaction GetTransaction(int timeOut);

		QueryAdapterBase CreateQueryAdapter();
		ParameterListBase CreateParameterList();
	}
}
