using MyLib.Data.Common;

namespace Demo.Data.Tables.Rows
{
	public partial class Product
	{
		public Product()
		{
		}

		public Product(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
