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
			return GetEntity(GetQueryKey(key));
		}

		private QueryBase GetQueryKey(object key)
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
			return q;
		}

		public T Get(ListFilter listFilter)
		{
			return GetEntity(GetQueryFilter(listFilter));
		}

		private QueryBase GetQueryFilter(
			ListFilter listFilter
			, OrderList orderList
		)
		{
			var q =
				new SqlQuery(
					$@"
					SELECT * 
					FROM {GetTableName()}
					WHERE {GetWhereFilter(listFilter)}
					{GetOrder(orderList)}
					"
				);
			listFilter
				.ForEach(
					f => q.Parameters.Add(f.Name, f.Value)
				);
			return q;
		}

		private static string GetOrder(
			OrderList orderList
		) => 
			orderList.Count == 0
			? ""
			: " ORDER BY "
				+ orderList
				.Select(o => $" {o.Name} {(o.Ascending?"ASC":"DESC")} ")
				.JoinWith(", ");

		private QueryBase GetQueryFilter(
			ListFilter listFilter
		)
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
			return q;
		}

		public IEnumerable<T> Select(object key)
		{
			return GetEnumerable(GetQueryKey(key));
		}

		public IEnumerable<T> Select(
			ListFilter listFilter
		)
		{
			return 
				GetEnumerable(
					GetQueryFilter(listFilter)
				);
		}

		public IEnumerable<T> Select(
			ListFilter listFilter
			, OrderList orderList
		)
		{
			return
				GetEnumerable(
					GetQueryFilter(
						listFilter
						, orderList
					)
				);
		}

		private static string GetWhereFilter(
			IEnumerable<Filter> iEnum
		)
		{
			return 
				iEnum
				.Select(p => $"{p.Name} = @{p.Name}")
				.JoinWith(" AND ");
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

		protected IEnumerable<T> GetEnumerable(
			IDataReader dr
			, bool close = true
			)
		{
			return
				GetEnumerable<T>(
					dr
					, close
				);
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
