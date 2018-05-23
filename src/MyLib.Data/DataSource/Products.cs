using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.Model;

namespace MyLib.Data.DataSource
{
	public class Products : Table<Product>
	{
		public Products(IDataBase dataBase) : base(dataBase)
		{
		}
	}
}
