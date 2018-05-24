namespace MyLib.Data.Common
{
	public interface IField
	{
		string Name { get;  }
		int Size { get;  }
		byte Precision { get; }
		byte Scale { get;  }
		object Value { get; set; }
	}
}
