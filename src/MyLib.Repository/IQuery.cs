using System.Collections.Generic;

namespace MyLib.Repository
{
	public interface IQuery<T>
	{
		T GetItem(object id);
		IEnumerable<T> SelectAll();
	}
}
