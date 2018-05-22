using System.Data;

namespace MyLib.Data.Common
{
	public interface IConnectionFactory 
	{
		IDbConnection GetConnection();
		IDbConnection GetConnection(int timeOut);
		IDbTransaction GetTransaction();
		IDbTransaction GetTransaction(int timeOut);

	}
}
