using System.Data;
using System.Data.SqlClient;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
    public class QueryAdatper : QueryAdapterBase
    {
		private const int DefaultTimeOut = 30;

		protected DataBaseAdapter DataBase { get; set; }

	    public QueryAdatper(
			DataBaseAdapter dataBase
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
			, ParameterListBase parameterlist
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
		    , ParameterListBase parameterlist
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
			, ParameterListBase parameterList
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

		public override IDataReader GetDataReader(
			string sql
			)
	    {
			return 
				GetDataReader(
					GetSqlCommand(sql)
				);
		}

	    public override IDataReader GetDataReader(
			string sql
			, ParameterListBase parameterList
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

	    public override IDataReader GetDataReader(
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

	    public override IDataReader GetDataReader(
			string sql
			, ParameterListBase parameterList
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

	    public override IDataReader GetDataReader(
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

	    public override IDataReader GetDataReader(
			string sql
			, ParameterListBase parameterList
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

	    public override IDataReader GetDataReader(
			string sql
			, ParameterListBase parameterList
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

	    public override int Execute(
			string sql
		)
	    {
		    return 
				GetSqlCommand(sql)
				.ExecuteNonQuery();
	    }

	    public override int Execute(
			string sql
			, ParameterListBase parameterList
		)
	    {
		    return
			    GetSqlCommand(
					sql
					, parameterList
				)
				.ExecuteNonQuery();
	    }

	    public override int Execute(
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

	    public override int Execute(
			string sql
			, ParameterListBase parameterList
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

	    public override int Execute(
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

	    public override int Execute(
			string sql
			, ParameterListBase parameterList
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

	    public override void BulkCopy(
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

	    public override void BulkCopy(
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

	    public override void BulkCopy(
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

	    private static T Get<T>(IDataReader dr)
	    {
			var t = default(T);
		    if(dr.Read())
		    {
			    t = (T)dr[0];
		    }
		    dr.Close();
		    return t;
		}

		public override T Get<T>(string sql)
		{
			return 
				Get<T>(
					GetDataReader(sql)
				);
		}

	    public override T Get<T>(
			string sql
			, ParameterListBase parameterList
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

	    public override T Get<T>(
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

	    public override T Get<T>(
			string sql
			, ParameterListBase parameterList
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

	    public override T Get<T>(
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

	    public override T Get<T>(
			string sql
			, ParameterListBase parameterList
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
