using MyLib.Data.Common;
using System;
using System.Data;
using Npgsql;

namespace MyLib.Data.PostgreSql
{
	public class PostDataBaseAdapter : IDataBaseAdapter
	{

		private const byte DefaultTimeOut = 30;
		private const byte DefaultCommandTimeOut = 30;
		private const int DefaultPort = 5432;

		private string DataSource { get; set; }
		private string DbName { get; set; }
		private string User { get; set; }
		private string Password { get; set; }
		private int Port { get; set; } = DefaultPort;
		private bool WindowsAuthentication { get; set; }

		public PostDataBaseAdapter(string pipeCnn)
		{
			var arr = pipeCnn.Split('|');
			switch (arr.Length)
			{
				case 2:
					SetCnnStr(
						arr[0]
						, arr[1]
					);
					break;
				case 3:
					SetCnnStr(
						arr[0]
						, arr[1]
						, Convert.ToInt32(arr[2])
					);
					break;
				case 4:
					SetCnnStr(
						arr[0]
						, arr[1]
						, arr[2]
						, arr[3]
					);
					break;
				case 5:
					SetCnnStr(
						arr[0]
						, arr[1]
						, arr[2]
						, arr[3]
						, Convert.ToInt32(arr[4])
					);
					break;
				default:
					throw 
						new Exception(
							"Formato incorrecto de pipeCnn"
						);
			}
		}

		private void SetCnnStr(
			string dataSource
			, string dbName
			, int port = DefaultPort
		)
		{
			DataSource = dataSource;
			DbName = dbName;
			WindowsAuthentication = true;
			Port = port;
		}

		public PostDataBaseAdapter(
			string dataSource
			, string dbName
			, int port = DefaultPort
		)
		{
			SetCnnStr(
				dataSource
				, dbName
				, port
			);
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
			, int port = DefaultPort 
		)
		{
			DataSource = dataSource;
			DbName = dbName;
			User = user;
			Password = password;
			Port = port;
		}

		public IDbConnection GetConnection()
		{
			return GetConnection(DefaultTimeOut);
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

		public IDbConnection GetConnection(
			int timeOut
			, int commandTimeOut
		)
		{
			var cnn =
				new NpgsqlConnection(
					GetStrConexion(timeOut, commandTimeOut)
				);
			cnn.Open();
			return cnn;
		}

		public string GetStrConexion(
			int timeOut = DefaultTimeOut
			, int commandTimeOut = DefaultCommandTimeOut
		)
		{
			return 
				$"Server={DataSource};"
					+ $"Port={Port}"
					+ $"Database={DbName};"
					+ GetLoginCnnStr()
					+ $"CommandTimeout={commandTimeOut};"
					+ $"Timeout={timeOut}"
					;
		}

		private string GetLoginCnnStr()
		 =>
			WindowsAuthentication
			? "Integrated Security=true;"
			: $"User Id={User};"
				+ $"Password={Password};";

		public QueryAdapterBase CreateQueryAdapter()
		{
			return new PostQueryAdapter(this);
		}
	}
}
