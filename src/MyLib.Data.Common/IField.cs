using System;

namespace MyLib.Data.Common
{
	public interface IField
	{
		bool IsPrimaryKey { get; }
		bool IsIdentity { get; }
		bool AllowNulls { get; }
		string Name { get; }
		int Size { get; }
		byte Precision { get; }
		byte Scale { get; }
		object Value { get; set; }
		Type Type { get; }
		bool ConvertValue { get; }
	}
}
