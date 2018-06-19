﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Data.SqlServer;
using MyLib.Misc;

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
					.GetFields();

			var noKeyFields =
				fields
					.Where(p => !p.IsPrimaryKey)
					.ToList();

			var keys =
				fields
				.Where(p => p.IsPrimaryKey);

			return 
				QueryAdapter
				.Execute(
					new SqlQuery(
						$@"
							UPDATE {GetTableName()}
							SET {GetUpdateFieldList(noKeyFields)}
							WHERE {GetWhereAndFieldList(keys)}
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
								{GetInsertFieldList(fields)}
							) VALUES (
								{GetInsertParamList(fields)}
							)
						"
						, GetParameters(fields)
					)
				);
		}

		private string GetPrimaryKey()
		{
			var att =
				(PrimaryKey)
				Attribute
				.GetCustomAttribute(
					GetType()
					, typeof(PrimaryKey)
				);
			return att?.Name ?? GetType().Name;
		}

		private string GetTableName()
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

		private static string GetWhereAndFieldList(
			IEnumerable<PropertyField> fields
		)
		{
			return GetWhereFieldList(fields, " AND ");
		}

		private static string GetWhereOrFieldList(
			IEnumerable<PropertyField> fields
		)
		{
			return GetWhereFieldList(fields, " OR ");
		}

		private static string GetWhereFieldList(
			IEnumerable<PropertyField> fields
			, string separador
		)
		{
			return
				Concat(
					fields
					, (PropertyField f) 
						=> $"{f.Name} = @{f.Name}"
					, separador
				);
		}

		private static string GetUpdateFieldList(
			IEnumerable<PropertyField> fields
		)
		{
			return
				Concat(
					fields
					, (PropertyField f) 
						=> $"{f.Name} = @{f.Name}"
					, ", "
				);
		}

		private static string GetInsertFieldList(
			IEnumerable<PropertyField> fields
		) 
		{
			return
				Concat(
					fields
					, (PropertyField f) 
						=> $"{f.Name}"
					, ", "
				);
		}

		private static string GetInsertParamList(
			IEnumerable<PropertyField> fields
		)
		{
			return
				Concat(
					fields
					, (PropertyField f)
						=> $"@{f.Name}"
					, ", "
				);
		}

		private static string Concat<TF>(
			IEnumerable<TF> enumerable
			, Func<TF, string> f
			, string separador
		) 
		{
			return
				enumerable
				.Aggregate(
					""
					, (list, i)
						=> list == ""
						? f(i)
						: $"{list}{separador}{f(i)}"
				);
		}

		private ParameterListBase GetParameters(	
			IEnumerable<PropertyField> fields
		)
		{
			var list =
				QueryAdapter.CreateParameterList();

			fields
			.Foreach(list.Add);

			return list;
		}
	}
}