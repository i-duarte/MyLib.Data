using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Data.Model;
using MyLib.Data.SqlServer;

namespace MyLib.Data.DataSource
{
	[Table]
	public class Products 
		: TableAdapter<Product>
	{
		public Products(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}

		public Product GetItem(int idProduct)
		{
			return 
				GetEntity(
					"SELECT * "
						+ "FROM Products "
						+ "WHERE IdProduct = @idProduct"
					, new ParameterList
					{
						{"idProduct", idProduct}
					}
				);
		}
	}
}
