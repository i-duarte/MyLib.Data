using MyLib.Data.Common;

namespace MyLib.Data.Entity
{
	public class Row : Queryable 
	{
		public Row(IQuery query) : base(query)
		{
		}

		public Row() : base(null)
		{
		}
	}
}
