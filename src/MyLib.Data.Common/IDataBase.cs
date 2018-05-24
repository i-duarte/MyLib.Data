using System.Data;

namespace MyLib.Data.Common
{
	public interface IDataBase
	{
		IDbConnection GetConnection();
		IDbConnection GetConnection(int timeOut);
		IDbTransaction GetTransaction();
		IDbTransaction GetTransaction(int timeOut);

		QueryAdapterBase GetNewQuery();

		ParameterListBase GetParameterList();
	}
}
