using System.Data;

namespace MyLib.Data.Common
{
	public interface IDataBaseAdapter
	{
		IDbConnection GetConnection();
		IDbConnection GetConnection(int timeOut);
		IDbConnection GetConnection(int timeOut, int commandTimeOut);
		QueryAdapterBase CreateQueryAdapter();
		
	}
}
