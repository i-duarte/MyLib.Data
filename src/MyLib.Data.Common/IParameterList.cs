using System;
using System.Collections.Generic;
using System.Data;

namespace MyLib.Data.Common
{
	public interface IParameterList 
		: IList<IDbDataParameter> 
	{
		void Add(string name, byte value);
		void Add(string name, short value);
		void Add(string name, int value);
		void Add(string name, long value);
		void Add(string name, float value);
		void Add(string name, double value);
		void Add(string name, decimal value);
		void Add(string name, string value, int size);
		void AddChar(string name, string value, int size);
		void Add(string name, DateTime value);
		void Add(string name, TimeSpan value);
	}
}
