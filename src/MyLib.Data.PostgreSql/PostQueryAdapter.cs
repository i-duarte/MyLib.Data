using System;
using System.Data;
using MyLib.Data.Common;
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

		public override void BulkCopy(IDataReader reader, string table, int numFields, bool deleteRecords = true)
		{
			throw new NotImplementedException();
		}

		public override void BulkCopy(IDataReader reader, string table, int numFields, IDbTransaction trans, bool deleteRecords = true)
		{
			throw new NotImplementedException();
		}

		public override void BulkCopy(DataTable dt, string table, int numFields, bool deleteRecords = true)
		{
			throw new NotImplementedException();
		}

		public override void BulkCopy(DataTable dt, string table, int numFields, IDbTransaction trans, bool deleteRecords = true)
		{
			throw new NotImplementedException();
		}

		public override ParameterListBase CreateParameterList()
		{
			return new PostParameterList();
		}

		public override ParameterListBase CreateParameterList<T>(string name, T t)
		{
			return new PostParameterList(name, t);
		}

		public override int Execute(QueryBase query)
		{
			return
			   GetPostCommand(query)
			   .ExecuteNonQuery();
		}

		private NpgsqlCommand GetPostCommand(
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
							GetPostCommand(
								query.Sql
							);
					}
					else
					{
						return
							GetPostCommand(
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
							GetPostCommand(
								query.Sql
								, query.Connection
							);
					}
					else
					{
						return
							GetPostCommand(
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
						GetPostCommand(
							query.Sql
							, query.Transaction
						);
				}
				else
				{
					return
						GetPostCommand(
							query.Sql
							, query.Parameters
							, query.Transaction
						);
				}
			}
		}

		private NpgsqlCommand GetPostCommand(
			string sql
			, IDbConnection connection
		)
		{
			throw new NotImplementedException();
		}

		private NpgsqlCommand GetPostCommand(
			string sql
			, IDbTransaction transaction
		)
		{
			throw new NotImplementedException();
		}

		private NpgsqlCommand GetPostCommand(
			string sql
			, ParameterListBase parameters
			, IDbTransaction transaction
		)
		{
			throw new NotImplementedException();
		}

		private NpgsqlCommand GetPostCommand(
			string sql
			, ParameterListBase parameters
			, IDbConnection connection
		)
		{
			throw new NotImplementedException();
		}

		private NpgsqlCommand GetPostCommand(
			string sql
			, ParameterListBase parameters
		)
		{
			throw new NotImplementedException();
		}

		private NpgsqlCommand GetPostCommand(
			string sql
			, int timeOut = DefaultTimeOut
		)
		{
			var cmd =
				new NpgsqlCommand(
						sql
						, GetConnection()
					)
				{
					CommandTimeout = timeOut
				};
			return cmd;
		}

		private NpgsqlConnection GetConnection()
		{
			throw new NotImplementedException();
		}

		public override T Get<T>(QueryBase query)
		{
			throw new NotImplementedException();
		}

		public override IDataReader GetDataReader(QueryBase query)
		{
			throw new NotImplementedException();
		}
	}
}
