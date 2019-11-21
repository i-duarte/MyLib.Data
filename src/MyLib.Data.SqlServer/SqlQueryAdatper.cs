using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Extensions.Linq;

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
				new SqlCommand(
					sql
					, (SqlConnection)transaction.Connection
					, (SqlTransaction)transaction
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
			using (var cmd = GetSqlCommand(query))
			{
				return cmd.ExecuteNonQuery();
			}
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

			using (
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
			    var bc = 
					GetBulkCopy(
						table
						, trans as SqlTransaction
					)
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

		public override void BulkCopy(
			DataTable dt
			, string table
			, int numFields
			, IDbTransaction trans
			, bool deleteRecords = true
		)
		{
			if (deleteRecords)
			{
				Execute(new SqlQuery("DELETE FROM " + table, trans));
			}

			using (
				var bc = GetBulkCopy(table, trans as SqlTransaction)
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

		public override ParameterListBase CreateParameterList<T>(string name, T t)
		{
			return new SqlParameterList(name, t);
		}


		public override QueryBase CreateQueryGet(
			string tableName
			, string keyName
			, object keyValue
		)
		{
			return 
				new SqlQuery(
				$@"
					SELECT * 
					FROM {tableName}
					WHERE {keyName} = @{keyName}					
				"
				, keyName
				, keyValue
			);
		}

		public override QueryBase CreateQuerySelect(
			string tableName
		) => 
			new SqlQuery(
				$@"
					SELECT * 
					FROM {tableName}
				"
			);

		public override QueryBase CreateQueryUpdate(
			string tableName
			, List<IField> fields
			, IDbTransaction transaction
		) => 
			new SqlQuery(
				$@"
					UPDATE {tableName}
					SET 
						{
							fields
							.Where(p => !p.IsPrimaryKey)
							.SelectUpdate()
						}
					WHERE 
						{
							fields
							.Where(p => p.IsPrimaryKey)
							.JoinWithAnd()
						}
				"
				, GetParameters(fields)
				, transaction
			);

		private ParameterListBase GetParameters(
			IEnumerable<IField> fields
		) =>
			CreateParameterList()
			.AddRange(fields);

		public override QueryBase CreateQuerySelect(
			string tableName
			, ListFilter listFilter
			, OrderList orderList = null
		)
		{
			return
				new SqlQuery(
					$@"
						SELECT * 
						FROM {tableName}
						WHERE {GetWhereFilter(listFilter)}
						{GetOrder(orderList)}
					"
					, listFilter
				)
				;
		}

		private string GetWhereFilter(
			IEnumerable<Filter> iEnum
		)
		{
			return
				iEnum
				.Select(p => $"{p.Name} = @{p.Name}")
				.JoinWithAnd();
		}

		private string GetOrder(
			OrderList orderList
		) =>
			orderList == null
			? ""
			: orderList.Count == 0
				? ""
				: " ORDER BY "
					+ orderList
					.Select(o => $" {o.Name} {(o.Ascending ? "ASC" : "DESC")} ")
					.JoinWith(", ");

		public override QueryBase CreateQueryInsert(
			string tableName
			, List<IField> fields
		) => 
			new SqlQuery(
				$@"
					INSERT INTO {tableName}(
						{fields.SelectInsertField()}
					) VALUES (
						{fields.SelectInsertParam()}
					)
				"
				, GetParameters(fields)
			);

		public override QueryBase CreateQueryDelete(
			string tableName
			, string keyName
		)
		{
			return
				new SqlQuery(
				$@"
					DELETE 
					FROM {tableName}
					WHERE {keyName} = @{keyName}
				"
				);
		}
	}
}
