using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
	public class ParameterList 
		: List<SqlParameter>, IParameterList
	{
		public ParameterList()
		{ }

		public ParameterList(
			string name
			, int value
		)
		{
			Add(name, value);
		}

		public ParameterList(
			string name
			, int size
			, string value
		)
		{
			Add(name, value, size);
		}

		public ParameterList(
			string name
			, DateTime value
		)
		{
			Add(name, value);
		}

		public ParameterList(
			string name
			, decimal value
		)
		{
			Add(name, value);
		}

		public ParameterList(
			string name
			, decimal value
			, byte precision
			, byte scale
		)
		{
			Add(name, value, precision, scale);
		}

		public ParameterList(
			string name
			, bool value
		)
		{
			Add(name, value);
		}

		public void AddChar(
			string name
			, string value
			, int size
		)
		{
			AddParameter(
				name
				, SqlDbType.Char
				, value
				, size
			);
		}

		public void Add(
			string name
			, byte value
			)
		{
			AddParameter(
				name
				, SqlDbType.TinyInt
				, value
			);
		}

		public void Add(
			string name
			, short value
			)
		{
			AddParameter(
				name
				, SqlDbType.SmallInt
				, value
			);
		}

		public void Add(
			string name
			, int value
		)
		{
			AddParameter(
				name
				, SqlDbType.Int
				, value
			);
		}

		public void AddInt(
			string name
			, int? idPadre
		)
		{
			if(idPadre == null)
			{
				AddParameter(
					name
					, SqlDbType.Int
				);
			}
			else
			{
				Add(name, idPadre.Value);
			}
		}

		public void Add(
			string name
			, long value
		)
		{
			AddParameter(
				name
				, SqlDbType.BigInt
				, value
			);
		}

		public void Add(
			string name
			, float value
			)
		{
			AddParameter(
				name
				, SqlDbType.Real
				, value
			);
		}

		public void Add(
			string name
			, double value
		)
		{
			AddParameter(
				name
				, SqlDbType.Float
				, value
			);
		}

		public void Add(
			string name
			, bool value
		)
		{
			AddParameter(
				name
				, SqlDbType.Bit
				, value
			);
		}

		public void Add(
			string name
			, decimal value
			)
		{
			AddParameter(
				name
				, SqlDbType.Decimal
				, value
			);
		}

		public void Add(
			string name
			, decimal value
			, byte precision
			, byte scale
			)
		{
			AddParameter(
				name
				, SqlDbType.Decimal
				, value
				, precision
				, scale
			);
		}

		public void Add(
			string name
			, string value
			, int size
			)
		{
			AddParameter(
				name
				, SqlDbType.VarChar
				, value
				, size
			);
		}

	    public void Add(
			string name
			, DateTime value
		)
		{
			AddParameter(
				name
				, SqlDbType.DateTime
				, value
			);
		}

		public void Add(
			string name
			, TimeSpan value
		)
		{
			AddParameter(
				name
				, SqlDbType.Time
				, value
			);
		}

		private void AddParameter(
			string name,
			SqlDbType type,
			object value
		)
		{
			var param = 
				new SqlParameter(name, type)
				{
					Direction = ParameterDirection.Input,
					Value = value
				};
			Add(param);
		}

		private void AddParameter(
			string name,
			SqlDbType type,
			object value,
			byte presicion,
			byte scale
		)
		{
			var param = 
				new SqlParameter(name, type)
				{
					Direction = ParameterDirection.Input,
					Precision = presicion,
					Scale = scale,
					Value = value
				};
			Add(param);
		}

		private void AddParameter(
			string name
			, SqlDbType type
			, object value
			, int size			
		)
		{
			var param = 
				new SqlParameter(name, type)
				{
					Direction = ParameterDirection.Input,
					Size = size,
					Value = value
				};
			Add(param);
		}

		private void AddParameter(string name, SqlDbType type)
		{
			var param =
				new SqlParameter(name, type)
				{
					Direction = ParameterDirection.Input
				};
			Add(param);
		}
	}
}
