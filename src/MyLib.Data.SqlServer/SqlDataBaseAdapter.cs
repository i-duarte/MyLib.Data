using System.Data;
using System.Data.SqlClient;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
	public class SqlDataBaseAdapter : IDataBaseAdapter
	{
		private const byte TimeOutDefault = 30;

		private string DataSource { get; set; }
		private string DbName { get; set; }
		private string User { get; set; }
		private string Password { get; set; }
		private bool WindowsAuthentication { get; set; }

		public SqlDataBaseAdapter(
			string dataSource
			, string dbName
			, string user
			, string password
		)
		{
			DataSource = dataSource;
			DbName = dbName;
			User = user;
			Password = password;
			WindowsAuthentication = false;
		}

		public SqlDataBaseAdapter(
			string dataSource
			, string dbName
		)
		{
			DataSource = dataSource;
			DbName = dbName;
			WindowsAuthentication = true;
		}

		public IDbConnection GetConnection()
		{
			return GetConnection(TimeOutDefault);
		}

		public IDbConnection GetConnection(
			int timeOut
		)
		{
			var cnn = new SqlConnection(
				GetStrConexion(timeOut));
			cnn.Open();
			return cnn;
		}

		public IDbTransaction GetTransaction()
		{
			var cnn = GetConnection();
			return cnn.BeginTransaction();
		}

		public IDbTransaction GetTransaction(
			int timeOut
		)
		{
			var cnn = GetConnection(timeOut);
			return cnn.BeginTransaction();
		}

		public string GetStrConexion(
			int timeOut = TimeOutDefault
		)
		{
			if(WindowsAuthentication)
			{
				return "Server=" + DataSource + ";" +
				       "Database=" + DbName + ";" +
				       "Trusted_Connection=True;" +
				       "Pooling=false;" +
				       "connection timeout=" + timeOut + ";";
			}

			return "Data Source=" + DataSource + ";"
			       + "Initial Catalog=" + DbName + ";"
			       + "User ID=" + User + ";"
			       + "Password=" + Password + ";"
			       + "Pooling=false;"
			       + "connection timeout=" + timeOut + ";";
		}

		public QueryAdapterBase CreateQueryAdapter()
		{
			return new SqlQueryAdatper(this);
		}

		

		
	}
}
