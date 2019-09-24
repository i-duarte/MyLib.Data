using MyLib.Data.Common;
using System;
using System.Data;
using Npgsql;

namespace MyLib.Data.PostgreSql
{
	public class PostDataBaseAdapter : IDataBaseAdapter
	{

		private const byte TimeOutDefault = 30;

		private string DataSource { get; set; }
		private string DbName { get; set; }
		private string User { get; set; }
		private string Password { get; set; }

		public PostDataBaseAdapter(string pipeCnn)
		{
			var arr = pipeCnn.Split('|');
			switch (arr.Length)
			{
				case 4:
					SetCnnStr(arr[0], arr[1], arr[2], arr[3]);
					break;
				default:
					throw new Exception("Formato incorrecto de pipeCnn");
			}
		}

		public PostDataBaseAdapter(
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
			, string user
			, string password
		)
		{
			DataSource = dataSource;
			DbName = dbName;
			User = user;
			Password = password;
		}

		public IDbConnection GetConnection()
		{
			return GetConnection(TimeOutDefault);
		}

		public IDbConnection GetConnection(
			int timeOut
		)
		{
			var cnn = 
				new NpgsqlConnection(
					GetStrConexion(timeOut)
				);
			cnn.Open();
			return cnn;
		}

		//public IDbTransaction GetTransaction()
		//{
		//	var cnn = GetConnection();
		//	return cnn.BeginTransaction();
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
			return "Data Source=" + DataSource + ";"
				   + "Initial Catalog=" + DbName + ";"
				   + "User ID=" + User + ";"
				   + "Password=" + Password + ";"
				   + "Pooling=false;"
				   + "connection timeout=" + timeOut + ";";
		}

		public QueryAdapterBase CreateQueryAdapter()
		{
			return new PostQueryAdapter(this);
		}
	}
}
