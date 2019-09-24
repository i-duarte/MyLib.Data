using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.SqlServer;

namespace MyLib.Data.EntityFramework
{
	public class TableAdapter<T> 
		: EntityDataSource<T> 
			where T : Entity, new()
	{
		public TableAdapter(
			IDataBaseAdapter dataBase
			) 
			: base(dataBase)
		{
		}

		public int Update(T row)
		{
			return Update(row, null);
		}

		public int Update(
			T row
			, IDbTransaction transaction
		)
		{
			var fields = 
				row
				.GetFields()
				.ToList();

			return 
				QueryAdapter
				.Execute(
					new SqlQuery(
						$@"
							UPDATE {GetTableName()}
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
					)
				);			
		}

		public int Insert(
			T entity
		)
		{
			var fields = 
				entity
					.GetFields()
					.Where(p=> !p.IsIdentity)
					.ToList();

			return
				QueryAdapter
				.Execute(
					new SqlQuery(
						$@"
							INSERT INTO {GetTableName()}(
								{fields.SelectInsertField()}
							) VALUES (
								{fields.SelectInsertParam()}
							)
						"
						, GetParameters(fields)
					)
				);
		}

		public int Delete(object key)
		{
			var q =
				new SqlQuery(
				$@"
					DELETE 
					FROM {GetTableName()}
					WHERE {GetPrimaryKey()} = @{GetPrimaryKey()}
				"
				);
			q.Parameters.Add(GetPrimaryKey(), key);
			return QueryAdapter.Execute(q);
		}

		private ParameterListBase GetParameters(
			IEnumerable<PropertyField> fields
		) => 
			QueryAdapter
			.CreateParameterList()
			.AddRange(fields);
	}
}
