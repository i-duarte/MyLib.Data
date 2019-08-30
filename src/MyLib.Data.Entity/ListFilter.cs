using System.Collections.Generic;

namespace MyLib.Data.EntityFramework
{
	public class ListFilter : List<Filter>
	{
		public void Add(
			string name
			, object value
		)
		{
			Add(
				new Filter{
					Name = name
					, Value = value
				}
			);
		}
	}
}
