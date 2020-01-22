using System;
using MyLib.Data.Common;

namespace MyLib.Data.PostgreSql
{
	internal class PostParameterList
		: ParameterListBase
	{
		public PostParameterList()
		{
		}

		public PostParameterList(string name, object value)
		{
			Add(name, value);
		}

		public override void Add(string name, bool value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, byte value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, short value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, int value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, long value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, float value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, double value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, decimal value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, decimal value, byte precision, byte scale)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, string value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, string value, int size)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, DateTime value)
		{
			throw new NotImplementedException();
		}

		public override void Add(string name, TimeSpan value)
		{
			throw new NotImplementedException();
		}

		public override void AddChar(string name, string value)
		{
			throw new NotImplementedException();
		}

		public override void AddChar(string name, string value, int size)
		{
			throw new NotImplementedException();
		}

		public override void AddInt(string name, int? idPadre)
		{
			throw new NotImplementedException();
		}
	}
}