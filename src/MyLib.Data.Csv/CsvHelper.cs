using MyLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MyLib.Data.Csv
{
	public class CsvHelper
	{
		public IEnumerable<T> GetEnumerable<T>(
			string path
			, string separador = ","
		) where T : new()
		{
			var lineas = 
				File.ReadAllLines(path);

			var nombresCampos =
				GetNombresCampos(lineas[0], separador);

			var fields = 
				GetFields(typeof(T))
				.Select(
					field =>
					{
						field.Index = 
							GetIndex(
								field.Name
								, nombresCampos
							);
						return field;
					}
				);

			return 
				lineas
				.Skip(1)
				.Select(
					linea => 
						GetEntity<T>(
							linea
							, fields
							, separador
						)
				);			
		}

		private T GetEntity<T>(
			string linea
			, IEnumerable<ICsvField> fields
			, string separador
		) where T:new()
		{
			var t = new T();
			var arr = CsvSplit(linea, separador);
			foreach(var field in fields)
			{
				field.Value =
					AutoConvert(
						arr[field.Index]
						, field.GetType()
					);
			}
			return t;
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

		private object[] CsvSplit(string linea, string separador)
		{
			throw new NotImplementedException();
		}

		private int GetIndex(
			string keyword
			, string[] list
		)
		{
			var index = 0;
			var nombreBusqueda = keyword.ToUpper();
			var nombresCampos = list.Select(i=>i.ToUpper());

			foreach (var nombre in nombresCampos)
			{
				if (nombre.IndexOf("|") >= 0)
				{
					if (nombreBusqueda.In(nombre.Split('|')))
					{
						return index;
					}
				}
				else
				{
					if (nombreBusqueda == nombre)
					{
						return index;
					}
				}
				index++;
			}
			
			return -1;
		}

		private string[] GetNombresCampos(
			string encabezado
			, string separador
		)
		{
			return 
				encabezado.Split(
					new[] { separador }
					, StringSplitOptions.RemoveEmptyEntries
				);
		}

		public IEnumerable<ICsvField> GetFields(
			Type type
		)
		{
			return
				type
				.GetProperties()
				.Where(
					p =>
						Attribute.IsDefined(
							p
							, typeof(ICsvField)
						)
				)
				.Select(
					p =>
					new PropertyField(this, p)
				);
		}
	}

	internal class PropertyField : ICsvField
	{
		private CsvHelper csvHelper;
		private PropertyInfo p;

		public string Name { get; set; }
		public int Index { get; set; }
		public object Value { get; set; }

		public PropertyField(
			CsvHelper csvHelper
			, PropertyInfo p
		)
		{
			this.csvHelper = csvHelper;
			this.p = p;
		}
	}

	internal class CsvField
	{
	}

	public interface ICsvField
	{
		string Name { get; set; }
		int Index { get; set; }
		object Value { get; set; }
	}
}
