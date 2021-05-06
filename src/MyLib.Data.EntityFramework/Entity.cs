using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Extensions.XLinq;

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
			.ForEach(
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
							else
							{
								throw new Exception(
									$"Valor nulo en propiedad que no acepta nulos " 
									+ $"[{GetType().Name}->{field.Name}]"
								);
							}
						}
						else
						{
							if (field.ConvertValue)
							{
								field.Value =
									AutoConvert(
										dr[field.Name]
										, field.Type
									);
							}
							else
							{
								field.Value = dr[field.Name];
							}
						}
						
					}
					catch(IndexOutOfRangeException e)
					{
						throw 
							new Exception(
								$"No se encontro el campo " 
									+ $"{field.Name} en {GetType().Name}"
								, e
							);
					}
					catch(Exception e)
					{
						throw 
							new Exception(
								$"Error al acceder al campo " 
									+ $"{field.Name} en {GetType().Name}"
								, e
							);
					}
				}
			);

		}

		private object AutoConvert(
			object v
			, Type type
		)
		{
			switch (type.ToString())
			{
				case "System.Byte":
					return Convert.ToByte(v);
				case "System.Int16":
					return Convert.ToInt16(v);
				case "System.Int32":
					return Convert.ToInt32(v);
				case "System.Int64":
				case "System.Nullable`1[System.Int64]":
					return Convert.ToInt64(v);				
				case "System.DateTime":
					return Convert.ToDateTime(v);
				case "System.Char":
					return Convert.ToChar(v);
				case "System.String":
					return Convert.ToString(v);
				case "System.Decimal":
				case "System.Nullable`1[System.Decimal]":
					return Convert.ToDecimal(v);
				case "System.Single":
					return Convert.ToSingle(v);
				case "System.Double":
					return Convert.ToDouble(v);
				default:
					throw 
						new ArgumentException(
							$"Tipo de dato inesperado {GetType()}"
						);
			}
		}

		public IEnumerable<IField> GetFields(
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
				.Select(
					p => 
					new PropertyField(this, p)
				);
		}		
	}
}
