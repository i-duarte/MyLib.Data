using System;
using System.Data;
using System.Data.SqlClient;
using MyLib.Data.Common;
using MyLib.Extensions.Linq;

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

		public SqlDataBaseAdapter(string pipeCnn)
		{
			var arr = pipeCnn.Split('|');
			switch (arr.Length)
			{
				case 2:
					SetCnnStr(arr[0], arr[1]);
					break;
				case 4:
					SetCnnStr(arr[0], arr[1], arr[2], arr[3]);
					break;
				default: 
					throw new Exception("Formato incorrecto de pipeCnn");
			}
		}

		public SqlDataBaseAdapter(
			string dataSource
			, string dbName
		)
		{
			SetCnnStr(dataSource, dbName);
		}

		public SqlDataBaseAdapter(
			string dataSource
			, string dbName
			, string user
			, string password
		)
		{
			SetCnnStr(
				dataSource
				, dbName
				, user
				, password
			);
		}

		protected void SetCnnStr(
			string dataSource
			, string dbName
		)
		{
			DataSource = dataSource;
			DbName = dbName;
			WindowsAuthentication = true;
		}

		protected void SetCnnStr(
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

		public IDbConnection GetConnection()
		{
			return GetConnection(TimeOutDefault);
		}

		private IDbConnection GetConnection(string strCnn)
		{
			return new SqlConnection(strCnn);
		}

		private void Open(IDbConnection cnn) => cnn.Open();

		public IDbConnection GetConnection(
			int timeOut
		) =>
			GetStrConexion(timeOut)
			.Pipe(GetConnection)
			.Pipe(Open);

		//var cnn = 
		//	new SqlConnection(
		//		GetStrConexion(timeOut)
		//	);
		//cnn.Open();
		//return cnn;

		//public IDbTransaction GetTransaction()
		//{
		//	return GetConnection().BeginTransaction();
		//}

		//public IDbTransaction GetTransaction(
		//	int timeOut
		//)
		//{
		//	var cnn = GetConnection(timeOut);
		//	return cnn.BeginTransaction();
		//}

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
