using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework.Attributes
{
	public class Field
		: NamedAttribute
			, IField
	{
		public int Size { get; }
		public byte Precision { get; }
		public byte Scale { get; }
		public object Value { get; set; }

		public Field()
		{
		}

		public Field(string name)
			: base(name)
		{
		}

		public Field(
			int size
		)
		{
			Size = size;
		}

		public Field(
			string name
			, int size
		) : base(name)
		{
			Size = size;
		}

		public Field(
			string name
			, byte precision
			, byte scale
		) : base(name)
		{
			Precision = precision;
			Scale = scale;
		}

		public Field(
			byte precision
			, byte scale
		)
		{
			Precision = precision;
			Scale = scale;
		}
	}
}
