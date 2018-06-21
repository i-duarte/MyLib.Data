using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;

namespace Demo.Data.Tables.Rows
{
	public class Product : Entity
	{

		[Field(IsPrimaryKey = true)]
		public int IdProduct { get; set; }

		[Field]
		public int IdBrand { get; set; }

		[Field(Size = 50)]
		public string Name { get; set; }

		[Field(Size = 400)]
		public string Description { get; set; }

		public Product()
		{
		}

		public Product(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
