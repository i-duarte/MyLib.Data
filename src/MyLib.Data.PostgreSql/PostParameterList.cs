using System;
using System.Data;
using Npgsql;
using NpgsqlTypes;
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

		public override void AddChar(
			string name
			, string value
		)
			=> AddChar(
				name
				, value
				, value.Length
			);

		public override void AddChar(
			string name
			, string value
			, int size
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Char
				, value
				, size
			);

		public override void Add(
			string name
			, byte value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Smallint
				, value
			);

		public override void Add(
			string name
			, short value
			)
		{
			AddParameter(
				name
				, NpgsqlDbType.Smallint
				, value
			);
		}

		public override void Add(
			string name
			, int value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Integer
				, value
			);

		public override void AddInt(
			string name
			, int? value
		)
		{
			if (value == null)
			{
				AddParameter(
					name
					, NpgsqlDbType.Integer
				);
			}
			else
			{
				Add(name, value.Value);
			}
		}

		public override void Add(
			string name
			, long value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Bigint
				, value
			);

		public override void Add(
			string name
			, float value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Real
				, value
			);

		public override void Add(
			string name
			, double value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Real
				, value
			);

		public override void Add(
			string name
			, bool value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Bit
				, value
			);

		public override void Add(
			string name
			, decimal value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Numeric
				, value
			);

		public override void Add(
			string name
			, decimal value
			, byte precision
			, byte scale
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Numeric
				, value
				, precision
				, scale
			);

		public override void Add(
			string name
			, string value
		)
			=> Add(name, value, value.Length);

		public override void Add(
			string name
			, string value
			, int size
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Varchar
				, value
				, size
			);

		public override void Add(
			string name
			, DateTime value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Date
				, value
			);

		public override void Add(
			string name
			, TimeSpan value
		)
			=> AddParameter(
				name
				, NpgsqlDbType.Time
				, value
			);

		private void AddParameter(
			string name,
			NpgsqlDbType type,
			object value
		)
		{
			var param =
				new NpgsqlParameter(name, type)
				{
					Direction = ParameterDirection.Input,
					Value = value
				};
			Add(param);
		}

		private void AddParameter(
			string name
			, NpgsqlDbType type
			, object value
			, byte presicion
			, byte scale
		)
		{
			var param =
				new NpgsqlParameter(name, type)
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
			, NpgsqlDbType type
			, object value
			, int size
		)
		{
			var param =
				new NpgsqlParameter(name, type)
				{
					Direction = ParameterDirection.Input,
					Size = size,
					Value = value
				};
			Add(param);
		}

		private void AddParameter(
			string name
			, NpgsqlDbType type
		)
		{
			var param =
				new NpgsqlParameter(name, type)
				{
					Direction = ParameterDirection.Input
				};
			Add(param);
		}
	}
}