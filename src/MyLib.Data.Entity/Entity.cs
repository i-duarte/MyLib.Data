using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Misc;

namespace MyLib.Data.EntityFramework
{
	public class Entity : DataAdapter
	{
		public Entity(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}

		public Entity() : base(null)
		{
		}

		public void Load(IDataReader dr)
		{
			GetFields()
			.Foreach(
				field =>
				{
					field.Value = dr[field.Name];
				}
			);
		}

		public IEnumerable<PropertyField> GetFields(
		)
		{
			return
				GetType()
				.GetProperties()
				.Where(
					p =>
						Attribute.IsDefined(
							p
							, typeof(Field)
						)
				)
				.Select(p => new PropertyField(this, p));
		}
	}
}
