using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;

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
					QueryAdapter
					.CreateQueryUpdate(
						GetTableName()
						, fields
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

			//TODO: CARGAR EL IDENTITY

			return
				QueryAdapter
				.Execute(
					QueryAdapter
					.CreateQueryInsert(
						GetTableName()
						, fields
					)
				);
		}

		public int Delete(object keyValue)
		{
			var keyName = GetPrimaryKey();
			var q =
				QueryAdapter
				.CreateQueryDelete(
					GetTableName()
					, keyName
				);				
			q.Parameters.Add(keyName, keyValue);
			return QueryAdapter.Execute(q);
		}

		
	}
}
