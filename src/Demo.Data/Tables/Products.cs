using Demo.Data.Tables.Rows;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework;

namespace Demo.Data.Tables
{
	public class Products : TableAdapter<Product>
	{
		public Products(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
