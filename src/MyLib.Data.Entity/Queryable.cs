using MyLib.Data.Common;

namespace MyLib.Data.Entity
{
	public class Queryable
	{
		protected IQuery Query { get; set; }

		public Queryable(IQuery query)
		{
			Query = query;
		}
	}
}
