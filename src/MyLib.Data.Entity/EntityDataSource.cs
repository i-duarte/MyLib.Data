using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Data.SqlServer;

namespace MyLib.Data.EntityFramework
{
	public class EntityDataSource<T> 
		: DataAdapter where T : Entity, new()
	{
		public EntityDataSource(
			IDataBaseAdapter dataBase
			) 
			: base(dataBase)
		{
		}

		public T Select(object key)
		{
			var q = 
				new SqlQuery(
				$@"
					SELECT * 
					FROM {GetTableName()}
					WHERE {GetPrimaryKey()} = @{GetPrimaryKey()}
				"
				);
			q.Parameters.Add(GetPrimaryKey(), key);
			return GetEntity<T>(q);
		}

		public IEnumerable<T> SelectAll()
		{
			return 
				GetEnumerable(
					new SqlQuery(
					$@"
						SELECT * 
						FROM {GetTableName()}
					"
					)
				);
		}

		protected TT GetEntity<TT>(
			QueryBase query
			) where TT : Entity, new()
		{
			var dr = 
				QueryAdapter
				.GetDataReader(
					query
				);

			TT e = null;
			if (dr.Read())
			{
				e = GetEntity<TT>(dr);
			}
			dr.Close();
			return e;			
		}

		private TT GetEntity<TT>(
			IDataReader dr
			) where TT : Entity, new()
		{
			var e = new TT();
			e.Load(dr);
			e.SetDataBase(DataBase);
			return e;
		}

		protected IEnumerable<T> GetEnumerable(
			QueryBase query
			)
		{
			return
				GetEnumerable<T>(
					QueryAdapter.GetDataReader(
						query
					)
				);
		}

		protected IEnumerable<TT> GetEnumerable<TT>(
			QueryBase query
			) where TT : Entity, new()
		{
			return
				GetEnumerable<TT>(
					QueryAdapter.GetDataReader(
						query
					)
				);
		}

		private IEnumerable<TT> GetEnumerable<TT>(
			IDataReader dr
			) where TT : Entity, new()
		{
			while(dr.Read())
			{
				yield return GetEntity<TT>(dr);
			}
			dr.Close();
		}

		protected string GetTableName()
		{
			var att =
				(Table)
				Attribute
				.GetCustomAttribute(
					GetType()
					, typeof(Table)
				);
			return att?.Name ?? GetType().Name;
		}

		protected string GetPrimaryKey()
		{
			return
				typeof(T)
				.GetProperties()
				.Where(
					p =>
						Attribute.IsDefined(
							p
							, typeof(Field)
						)
				)
				.Select(p => new PropertyField(p))
				.First(p => p.IsPrimaryKey)
				.Name;
		}
	}
}
