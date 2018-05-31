using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;

namespace MyLib.Data.Model
{
    public class Product : Entity
    {
		[PrimaryKey]
		[Identity]
		[Field]
		public int IdProduct { get; set; }

	    [Field]
		public int IdBrand { get; set; }

	    [Field(50)]
		public string Name { get; set; }

	    [Field(400)]
		public string Description { get; set; }
    }
}
