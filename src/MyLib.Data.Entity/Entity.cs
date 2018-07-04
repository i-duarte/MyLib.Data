using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Extensions.Collection;

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
					try
					{
						if(DBNull.Value == dr[field.Name])
						{
							if(field.AllowNulls)
							{
								field.Value = null;
							}

						}
						else
						{
							field.Value = dr[field.Name];
						}
						
					}
					catch(Exception e)
					{
						throw new Exception($"Error al acceder al campo {field.Name} en {GetType().Name}", e);
					}
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
