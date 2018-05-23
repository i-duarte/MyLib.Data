using System.Data;
using System.Data.SqlClient;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
    public class Query : IQuery
    {
		private const int DefaultTimeOut = 30;

		protected DataBase DataBase { get; set; }

	    public Query(
			DataBase dataBase
		)
	    {
		    DataBase = dataBase;
	    }

	    private SqlConnection GetConnection()
	    {
		    return 
				DataBase
				.GetConnection() as SqlConnection;
	    }

	    private SqlCommand GetSqlCommand(
			string sql
			, int timeOut = DefaultTimeOut
		)
	    {
		    return 
				new SqlCommand(
						sql
						, GetConnection()
					)
					.SetTimeOut(timeOut);
	    }

	    private SqlCommand GetSqlCommand(
			string sql
			, IParameterList parameterlist
		)
	    {
		    return 
			    GetSqlCommand(sql)
				.AddParameters(parameterlist);
	    }

	    private static SqlCommand GetSqlCommand(
			string sql
			, IDbConnection connection
		)
	    {
		    return 
				new SqlCommand(
						sql
						, connection as SqlConnection
					);
	    }

	    private static SqlCommand GetSqlCommand(
		    string sql
		    , IParameterList parameterlist
		    , IDbConnection connection
	    )
	    {
		    return
			    GetSqlCommand(
				    sql
				    , connection
			    )
				.AddParameters(parameterlist);
	    }

	    private static SqlCommand GetSqlCommand(
		    string sql
		    , IDbTransaction transaction
	    )
	    {
		    return 
				GetSqlCommand(
					sql
					, transaction.Connection
				);
	    }

	    private static SqlCommand GetSqlCommand(
		    string sql
			, IParameterList parameterList
		    , IDbTransaction transaction
			, int timeOut = DefaultTimeOut
	    )
	    {
		    return
			    GetSqlCommand(
					sql
					, transaction
				)
				.AddParameters(parameterList)
				.SetTimeOut(timeOut);
	    }

		private static SqlDataReader GetDataReader(
			SqlCommand cmd
		)
	    {
		    return 
				cmd.ExecuteReader(
					CommandBehavior.CloseConnection
				);
	    }

		public IDataReader GetDataReader(
			string sql
			)
	    {
			return 
				GetDataReader(
					GetSqlCommand(sql)
				);
		}

	    public IDataReader GetDataReader(
			string sql
			, IParameterList parameterList
		)
	    {
		    return 
				GetDataReader(
					GetSqlCommand(
						sql
						, parameterList
					)
				);
		}

	    public IDataReader GetDataReader(
			string sql
			, IDbConnection connection
		)
	    {
		    return 
				GetDataReader(
					GetSqlCommand(
						sql
						, connection
					)
				);
	    }

	    public IDataReader GetDataReader(
			string sql
			, IParameterList parameterList
			, IDbConnection connection
		)
	    {
		    return 
				GetDataReader(
					GetSqlCommand(
						sql
						, parameterList
						, connection
					)
				);
	    }

	    public IDataReader GetDataReader(
			string sql
			, IDbTransaction transaction
		)
	    {
		    return
			    GetDataReader(
					GetSqlCommand(
						sql
						, transaction
					)
				);
	    }

	    public IDataReader GetDataReader(
			string sql
			, IParameterList parameterList
			, IDbTransaction transaction
		)
	    {
		    return
			    GetDataReader(
				    GetSqlCommand(
					    sql
					    , parameterList
					    , transaction
				    )
			    );
	    }

	    public IDataReader GetDataReader(
			string sql
			, IParameterList parameterList
			, IDbTransaction transaction
			, int timeOut
			)
	    {
		    return 
				GetDataReader(
					GetSqlCommand(
						sql
						, parameterList
						, transaction
						, timeOut
					)
				);
	    }

	    public int Execute(
			string sql
		)
	    {
		    return 
				GetSqlCommand(sql)
				.ExecuteNonQuery();
	    }

	    public int Execute(
			string sql
			, IParameterList parameterList
		)
	    {
		    return
			    GetSqlCommand(
					sql
					, parameterList
				)
				.ExecuteNonQuery();
	    }

	    public int Execute(
			string sql
			, IDbConnection connection
		)
	    {
		    return 
				GetSqlCommand(
					sql
					, connection
				)
				.ExecuteNonQuery();
	    }

	    public int Execute(
			string sql
			, IParameterList parameterList
			, IDbConnection connection
		)
	    {
		    return
			    GetSqlCommand(
				    sql
					, parameterList
					, connection
				)
				.ExecuteNonQuery();
	    }

	    public int Execute(
			string sql
			, IDbTransaction transaction
			)
	    {
		    return
			    GetSqlCommand(
					sql
					, transaction
				)
				.ExecuteNonQuery();

	    }

	    public int Execute(
			string sql
			, IParameterList parameterList
			, IDbTransaction transaction
		)
	    {
		    return
			    GetSqlCommand(
					sql
					, parameterList
					, transaction
				)
				.ExecuteNonQuery();
	    }

	    private SqlBulkCopy GetBulkCopy(
			string tableName
			, SqlTransaction trans
			)
	    {
		    return 
				new SqlBulkCopy
				(
					trans.Connection,
					SqlBulkCopyOptions.Default,
					trans
				)
				{
					DestinationTableName = tableName
				}
				.SetTimeOut(3600);
	    }

	    private SqlBulkCopy GetBulkCopy(string tableName)
	    {
		    return 
				new SqlBulkCopy(GetConnection())
				{
					DestinationTableName = tableName
				}
				.SetTimeOut(3600);
	    }

	    public void BulkCopy(
			IDataReader reader
			, string table
			, int numFields
			, bool deleteRecords = true
		)
	    {
			if(deleteRecords)
		    {
			    Execute("DELETE FROM " + table);
		    }

		    using(
				var bc = GetBulkCopy(table)
			)
			{
				bc.MapColumns(numFields);
				bc.WriteToServer(reader);
			}
		}

	    public void BulkCopy(
			IDataReader reader
			, string table
			, int numFields
			, IDbTransaction trans
			, bool deleteRecords = true
		)
	    {
			if(deleteRecords)
		    {
			    Execute("DELETE FROM " + table, trans);
		    }

			using
		    (
			    var bc = GetBulkCopy(table, trans as SqlTransaction)
		    )
			{
				bc.MapColumns(numFields);
			    bc.WriteToServer(reader);
			    reader.Close();
		    }
		}

	    public void BulkCopy(
			DataTable dt
			, string table
			, int numFields
			, bool deleteRecords = true
		)
	    {
		    if (deleteRecords)
		    {
			    Execute("DELETE FROM " + table);
		    }

		    using(
				var bc = GetBulkCopy(table)
			)
			{
				bc.MapColumns(numFields);
				bc.WriteToServer(dt);
			}
			
		}

	    public static T Get<T>(IDataReader dr)
	    {
			var t = default(T);
		    if(dr.Read())
		    {
			    t = (T)dr[0];
		    }
		    dr.Close();
		    return t;
		}

		public T Get<T>(string sql)
		{
			return 
				Get<T>(
					GetDataReader(sql)
				);
		}

	    public T Get<T>(
			string sql
			, IParameterList parameterList
		)
	    {
			return 
				Get<T>(
					GetDataReader(
						sql
						, parameterList
					)
				);
		}

	    public T Get<T>(
			string sql
			, IDbConnection connection
			)
	    {
			return 
				Get<T>(
					GetDataReader(
						sql
						, connection
					)
				);
		}

	    public T Get<T>(
			string sql
			, IParameterList parameterList
			, IDbConnection connection
		)
	    {
			return 
				Get<T>(
					GetDataReader(
						sql
						, parameterList
						, connection
					)
				);
		}

	    public T Get<T>(
			string sql
			, IDbTransaction transaction
			)
	    {
			return 
				Get<T>(
					GetDataReader(
						sql
						, transaction
					)
				);
		}

	    public T Get<T>(
			string sql
			, IParameterList parameterList
			, IDbTransaction transaction
			)
	    {
			return 
				Get<T>(
					GetDataReader(
						sql
						, parameterList
						, transaction
					)
				);
		}
    }
}
