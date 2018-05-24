﻿using System;
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
		public abstract void Add(string name, string value, int size);
		public abstract void AddChar(string name, string value, int size);
		public abstract void Add(string name, DateTime value);
		public abstract void Add(string name, TimeSpan value);

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
