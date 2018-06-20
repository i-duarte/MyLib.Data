using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework.Attributes
{
	public class Field
		: NamedAttribute
			, IField
	{
		public bool IsPrimaryKey { get; set; }
		public bool IsIdentity { get; set; }
		public bool AllowNulls { get; set; }
		public int Size { get; set; }
		public byte Precision { get; set; }
		public byte Scale { get; set; }
		public object Value { get; set; }
	}
}
