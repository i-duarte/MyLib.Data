using System;
using System.Data;
using System.Data.SqlClient;
using MyLib.Data.Common;
using MyLib.Extensions.XLinq;

namespace MyLib.Data.SqlServer
{
	public class SqlDataBaseAdapter : IDataBaseAdapter
	{
		private const byte DefaultTimeOut = 30;
		private string DataSource {  get; set; }
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
		) => SetCnnStr(dataSource, dbName);

		public SqlDataBaseAdapter(
			string dataSource
			, string dbName
			, string user
			, string password
		) => 
			SetCnnStr(
				dataSource
				, dbName
				, user
				, password
			);

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
			=> GetConnection(DefaultTimeOut);

		private IDbConnection GetConnection(string strCnn) 
			=> new SqlConnection(strCnn);

		private void Open(IDbConnection cnn) 
			=> cnn.Open();

		public IDbConnection GetConnection(
			int timeOut
		) =>
			GetStrConexion(timeOut)
			.Pipe(GetConnection)
			.Pipe(Open);

		public IDbConnection GetConnection(
			int timeOut
			, int commandTimeOut
		) =>
			throw 
				new NotImplementedException(
					"Metodo no soportado"
				);

		public string GetStrConexion(
			int timeOut = DefaultTimeOut
		) => 
			$"Server={DataSource};" 
				+ $"Database={DbName};" 
				+ GetLoginCnnStr() 
				+ "Pooling=false;" 
				+ $"connection timeout={timeOut};"
				;

		private string GetLoginCnnStr(
		) => 
			WindowsAuthentication
			? "Trusted_Connection=True;"
			: $"User ID={User};"
				+ $"Password={Password};";

		public QueryAdapterBase CreateQueryAdapter()
		{
			return new SqlQueryAdatper(this);
		}
	}
}
