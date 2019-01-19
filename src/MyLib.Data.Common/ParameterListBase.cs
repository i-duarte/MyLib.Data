using System;
using System.Collections.Generic;
using System.Data;

namespace MyLib.Data.Common
{
	public abstract class ParameterListBase 
		: List<IDbDataParameter>
	{
		public abstract void Add(string name, bool value);
		public abstract void Add(string name, byte value);
		public abstract void Add(string name, short value);
		public abstract void Add(string name, int value);
		public abstract void AddInt(string name, int? idPadre);
		public abstract void Add(string name, long value);
		public abstract void Add(string name, float value);
		public abstract void Add(string name, double value);
		public abstract void Add(string name, decimal value);
		public abstract void Add(string name, decimal value, byte precision, byte scale);
		public abstract void Add(string name, string value);
		public abstract void Add(string name, string value, int size);
		public abstract void AddChar(string name, string value);
		public abstract void AddChar(string name, string value, int size);
		public abstract void Add(string name, DateTime value);
		public abstract void Add(string name, TimeSpan value);

		public void Add(string name, object value)
		{
			switch(value.GetType().ToString())
			{
				case "System.Byte":
					Add(name, (byte)value);
					break;
				case "System.Int16":
					Add(name, (short)value);
					break;
				case "System.Int32":
					Add(name, (int)value);
					break;
				case "System.DateTime":
					Add(name, (DateTime)value);
					break;
				case "System.Char":
					AddChar(name, (string)value);
					break;
				case "System.String":
					Add(name, (string)value);
					break;
				case "System.Decimal":
					Add(name, (decimal)value);
					break;
				case "System.Single":
					Add(name, (float)value);
					break;
				case "System.Double":
					Add(name, (double)value);
					break;
				default:
					throw new ArgumentException($"Tipo de dato inesperado {value.GetType()}");
			}
		}

		public void Add(
			IField field
		)
		{
			switch(field.Value.GetType().ToString())
			{
				case "System.Byte":
					Add(field.Name, (byte) field.Value);
					break;
				case "System.Int16":
					Add(field.Name, (short) field.Value);
					break;
				case "System.Int32":
					Add(field.Name, (int) field.Value);
					break;
				case "System.DateTime":
					Add(field.Name, (DateTime) field.Value);
					break;
				case "System.Char":
					AddChar(field.Name, (string) field.Value, field.Size);
					break;
				case "System.String":
					Add(field.Name, (string) field.Value, field.Size);
					break;
				case "System.Decimal":
					Add(
						field.Name
						, (decimal)field.Value
						, field.Precision
						, field.Scale
					);
					break;
				case "System.Single":
					Add(field.Name, (float) field.Value);
					break;
				case "System.Double":
					Add(field.Name, (double) field.Value);
					break;
				default:
					throw new ArgumentException("Tipo de dato inesperado");
			}
		}
	}
}
