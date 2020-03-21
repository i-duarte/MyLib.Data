using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Extensions.Linq;
using Npgsql;

namespace MyLib.Data.PostgreSql
{
	public class PostQueryAdapter : QueryAdapterBase
	{
		private const int DefaultTimeOut = 30;

		protected PostDataBaseAdapter DataBase { get; set; }

		public PostQueryAdapter(
			PostDataBaseAdapter dataBase
		)
		{
			DataBase = dataBase;
		}

		private NpgsqlConnection GetConnection()
		{
			return
				DataBase
				.GetConnection() as NpgsqlConnection;
		}

		private NpgsqlConnection GetConnection(int timeOut)
		{
			return
				DataBase
				.GetConnection(timeOut) as NpgsqlConnection;
		}

		private NpgsqlCommand GetNpgsqlCommand(
			string sql
			, int timeOut = DefaultTimeOut
		)
		{
			return
				new NpgsqlCommand(
						sql
						, GetConnection(timeOut)
					)
					;
		}

		private NpgsqlCommand GetNpgsqlCommand(
			string sql
			, ParameterListBase parameterlist
		)
		{
			return
				GetNpgsqlCommand(sql)
				.AddParameters(parameterlist);
		}

		private static NpgsqlCommand GetNpgsqlCommand(
			string sql
			, IDbConnection connection
		)
		{
			return
				new NpgsqlCommand(
						sql
						, connection as NpgsqlConnection
					);
		}

		private static NpgsqlCommand GetNpgsqlCommand(
			string sql
			, ParameterListBase parameterlist
			, IDbConnection connection
		)
		{
			return
				GetNpgsqlCommand(
					sql
					, connection
				)
				.AddParameters(parameterlist);
		}

		private static NpgsqlCommand GetNpgsqlCommand(
			string sql
			, ParameterListBase parameterlist
			, IDbTransaction transaction
		)
		{
			return
				GetNpgsqlCommand(
					sql
					, transaction
				)
				.AddParameters(parameterlist);
		}

		private static NpgsqlCommand GetNpgsqlCommand(
			string sql
			, IDbTransaction transaction
		)
		{
			return
				new NpgsqlCommand(
					sql
					, (NpgsqlConnection)
						transaction.Connection
					, (NpgsqlTransaction)
						transaction
				);
		}

		//private static NpgsqlCommand GetNpgsqlCommand(
		//	string sql
		//	, ParameterListBase parameterList
		//	, IDbTransaction transaction
		//	, int timeOut = DefaultTimeOut
		//)
		//{
		//	return
		//		GetNpgsqlCommand(
		//			sql
		//			, transaction
		//		)
		//		.AddParameters(parameterList)
		//		.SetTimeOut(timeOut);
		//}

		private static NpgsqlDataReader GetDataReader(
			NpgsqlCommand cmd
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
					GetNpgsqlCommand(query)
				);
		}

		public override int Execute(
			QueryBase query
		)
		{
			using (var cmd = GetNpgsqlCommand(query))
			{
				return cmd.ExecuteNonQuery();
			}
		}

		//private SqlBulkCopy GetBulkCopy(
		//	string tableName
		//	, NpgsqlTransaction trans
		//	)
		//{
		//	return
		//		new SqlBulkCopy
		//		(
		//			trans.Connection,
		//			SqlBulkCopyOptions.Default,
		//			trans
		//		)
		//		{
		//			DestinationTableName = tableName
		//		}
		//		.SetTimeOut(3600);
		//}

		//private SqlBulkCopy GetBulkCopy(string tableName)
		//{
		//	return
		//		new SqlBulkCopy(GetConnection())
		//		{
		//			DestinationTableName = tableName
		//		}
		//		.SetTimeOut(3600);
		//}

		public override void BulkCopy(
			IDataReader reader
			, string table
			, int numFields
			, bool deleteRecords = true
		)
			=> throw new NotImplementedException();

		private Npgsql.NpgsqlCopyTextWriter GetBulkCopy(
			string table
		)
			=> throw new NotImplementedException();

		public override void BulkCopy(
			IDataReader reader
			, string table
			, int numFields
			, IDbTransaction trans
			, bool deleteRecords = true
		)
			=> throw new NotImplementedException();

		public override void BulkCopy(
			DataTable dt
			, string table
			, int numFields
			, bool deleteRecords = true
		)
			=> throw new NotImplementedException();

		public override void BulkCopy(
			DataTable dt
			, string table
			, int numFields
			, IDbTransaction trans
			, bool deleteRecords = true
		)
			=> throw new NotImplementedException();

		private static T Get<T>(IDataReader dr)
		{
			var t = default(T);
			if (dr.Read())
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

		private NpgsqlCommand GetNpgsqlCommand(
			QueryBase query
		)
		{
			if (string.IsNullOrEmpty(query.Sql))
			{
				throw new ArgumentException(nameof(query));
			}

			if (query.Transaction == null)
			{
				if (query.Connection == null)
				{
					if (query.Parameters.Count == 0)
					{
						return
							GetNpgsqlCommand(
								query.Sql
							);
					}
					else
					{
						return
							GetNpgsqlCommand(
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
							GetNpgsqlCommand(
								query.Sql
								, query.Connection
							);
					}
					else
					{
						return
							GetNpgsqlCommand(
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
						GetNpgsqlCommand(
							query.Sql
							, query.Transaction
						);
				}
				else
				{
					return
						GetNpgsqlCommand(
							query.Sql
							, query.Parameters
							, query.Transaction
						);
				}
			}
		}

		public override ParameterListBase CreateParameterList()
		{
			return new PostParameterList();
		}

		public override ParameterListBase CreateParameterList<T>(string name, T t)
		{
			return new PostParameterList(name, t);
		}


		public override QueryBase CreateQueryGet(
			string tableName
			, string keyName
			, object keyValue
		)
		{
			return
				new PostQuery(
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
			new PostQuery(
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
			new PostQuery(
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
				new PostQuery(
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
			new PostQuery(
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
				new PostQuery(
				$@"
					DELETE 
					FROM {tableName}
					WHERE {keyName} = @{keyName}
				"
				);
		}
	}
}
