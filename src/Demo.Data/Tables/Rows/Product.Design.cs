using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;

namespace Demo.Data.Tables.Rows
{
	partial class Product : Entity
	{
		[Field(IsPrimaryKey = true)]
		public int IdProduct { get; set;}

		[Field]
		public int IdBrand { get; set; }

		[Field(Size = 50)]
		public string Name { get; set; }
		
		[Field(Size = 400)]
		public string Description { get; set; }
	}
}
