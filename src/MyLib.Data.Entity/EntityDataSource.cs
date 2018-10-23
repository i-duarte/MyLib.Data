using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Data.SqlServer;
using MyLib.Extensions.Linq;

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

		public T Get(object key)
		{
			var keyName = GetPrimaryKey();
			var q = 
				new SqlQuery(
				$@"
					SELECT * 
					FROM {GetTableName()}
					WHERE {keyName} = @{keyName}					
				"
				);
			q.Parameters.Add(keyName, key);
			return GetEntity<T>(q);
		}

		public IEnumerable<T> Select(object key)
		{
			var keyName = GetPrimaryKey();
			var q =
				new SqlQuery(
					$@"
					SELECT * 
					FROM {GetTableName()}
					WHERE {keyName} = @{keyName}					
				"
				);
			q.Parameters.Add(keyName, key);
			return GetEnumerable(q);
		}

		public T Select(List<Filter> listFilter)
		{
			var q =
				new SqlQuery(
				$@"
					SELECT * 
					FROM {GetTableName()}
					WHERE {GetWhereFilter(listFilter)}				
				"
				);
			listFilter
				.ForEach(
					f => q.Parameters.Add(f.Name, f.Value)
				);
			return GetEntity<T>(q);
		}

		private static string GetWhereFilter(
			IEnumerable<Filter> iEnum
		)
		{
			return
				iEnum
				.Select(p => $"{p.Name} = @{p.Name}")
				.Concat(" AND ");			
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

		protected T GetEntity(
			QueryBase query
		)
		{
			var dr =
				QueryAdapter
					.GetDataReader(
						query
					);

			T e = null;
			if (dr.Read())
			{
				e = GetEntity<T>(dr);
			}
			dr.Close();
			return e;
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

		protected IEnumerable<string> GetPrimaryKeyFieldNames()
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
				.Where(p => p.IsPrimaryKey)
				.Select(p => p.Name);
		}
	}
}
