using MyLib.Data.DataSource;
using MyLib.Data.SqlServer;

namespace MyLib.Data
{
	public class DemoDataBase : DataBase
	{
		private Products _products;
		public Products Products
			=> _products 
			?? (_products = new Products(this));

		public DemoDataBase(string dataSource, string dbName) 
			: base(dataSource, dbName)
		{
		}
	}
}
