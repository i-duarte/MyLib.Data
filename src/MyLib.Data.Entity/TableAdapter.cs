using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Misc;

namespace MyLib.Data.EntityFramework
{
	public class TableAdapter<T> 
		: EntityDataSource<T> 
			where T : Entity, new()
	{
		public TableAdapter(IDataBase dataBase) 
			: base(dataBase)
		{
		}

		public IEnumerable<T> Select()
		{
			var sql = 
				$@"
					SELECT * 
					FROM {GetTableName()}
				";
			return GetEnumerable(sql);
		}

		public int Insert(
			T entity
		)
		{
			var fields = 
				entity
					.GetFields()
					.ToList();

			var sql =
				$@"
					INSERT INTO {GetTableName()}(
						{GetFieldNameList(fields)}
					) VALUES(
						{GetFieldParamList(fields)}
					)
				";

			var param = GetParameters(fields);

			return Query.Execute(sql, param);
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

		private static string GetFieldNameList(
			IEnumerable<PropertyField> fields
		) 
		{
			return
				fields
				.Select(i => i.Name)
				.Aggregate(
					""
					, (res, item)
						=> res == ""
							? ""
							: $"{res}, @{item}"
				);
		}

		private static string GetFieldParamList(
			IEnumerable<PropertyField> fields
		)
		{
			return
				fields
				.Select(i => i.Name)
				.Aggregate(
					""
					, (res, item) 
						=> res == "" 
						? "" 
						: $"{res}, @{item}"
				);
		}

		private ParameterListBase GetParameters(	
			IEnumerable<PropertyField> fields
		)
		{
			var list = DataBase.GetParameterList();

			fields
			.Foreach(list.Add);

			return list;
		}
	}
}
