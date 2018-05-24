using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Data.Model;

namespace MyLib.Data.DataSource
{
	[Table]
	public class Products 
		: TableAdapter<Product>
	{
		public Products(IDataBase dataBase) 
			: base(dataBase)
		{
		}
	}
}
