using Demo.Data.Tables;
using MyLib.Data.SqlServer;

namespace Demo.Data
{
	public class StockData : SqlDataBaseAdapter
	{
		public StockData(string dataSource, string dbName) 
			: base(dataSource, dbName)
		{
		}

		public StockData(string dataSource, string dbName, string user, string password) 
			: base(dataSource, dbName, user, password)
		{
		}

		private Products _products;
		public Products Products 
			=> _products 
			?? (_products = new Products(this));
	}
}
