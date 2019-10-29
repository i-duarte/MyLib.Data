using System.Data;

namespace MyLib.Data.Common
{
	public interface IDataBaseAdapter
	{
		IDbConnection GetConnection();
		IDbConnection GetConnection(int timeOut);

		QueryAdapterBase CreateQueryAdapter();
		
	}
}
