using System.Collections.Generic;
using Demo.Data.Tables;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework;

namespace Demo.Data
{
	public class StockData 
		: DataBase
	{
		public StockData(IDataBaseAdapter dataBaseAdapter) 			
			: base(dataBaseAdapter)
		{
		}

		private Products _products;
		public Products Products 
			=> _products 
			?? (_products = new Products(DataBaseAdapter));

		private Clientes _clientes;
		public Clientes Clientes
			=> _clientes
			?? (_clientes = new Clientes(DataBaseAdapter));

	}
}
