using System;
using System.Data;
using System.Data.SqlClient;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
    public class SqlQueryAdatper : QueryAdapterBase
    {
		private const int DefaultTimeOut = 30;

		protected SqlDataBaseAdapter DataBase { get; set; }

	    public SqlQueryAdatper(
			SqlDataBaseAdapter dataBase
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
			QueryBase query
			)
	    {
			return 
				GetDataReader(
					GetSqlCommand(query)
				);
		}

	    public override int Execute(
			QueryBase query
		)
	    {
		    return
			    GetSqlCommand(
					query
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
			    Execute(new SqlQuery("DELETE FROM " + table));
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
			    Execute(new SqlQuery("DELETE FROM " + table, trans));
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
			    Execute(new SqlQuery("DELETE FROM " + table));
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

		public override T Get<T>(
			QueryBase query
			)
	    {
			return 
				Get<T>(
					GetDataReader(
						query
					)
				);
		}

		private SqlCommand GetSqlCommand(QueryBase query)
		{
			if (string.IsNullOrEmpty(query.Sql))
			{
				throw new ArgumentException(nameof(query));
			}

			if(query.Transaction == null)
			{
				if(query.Connection == null)
				{
					if(query.Parameters.Count == 0)
					{
						return 
							GetSqlCommand(
								query.Sql
							);
					}
					else
					{
						return 
							GetSqlCommand(
								query.Sql
								, query.Parameters
							);
					}
				}
				else
				{
					if (query.Parameters.Count == 0)
					{
						return 
							GetSqlCommand(
								query.Sql
								, query.Connection
							);
					}
					else
					{
						return
							GetSqlCommand(
								query.Sql
								, query.Parameters
								, query.Connection
							);
					}
				}
			}		
			else
			{
				if (query.Parameters.Count == 0)
				{
					return 
						GetSqlCommand(
							query.Sql
							, query.Transaction
						);
				}
				else
				{
					return 
						GetSqlCommand(
							query.Sql
							, query.Parameters
							, query.Transaction
						);
				}
			}			
		}

		public override ParameterListBase CreateParameterList()
		{
			return new SqlParameterList();
		}

		public override ParameterListBase CreateParameterList<T>(T t)
		{
			return new SqlParameterList(t);
		}
	}
}
