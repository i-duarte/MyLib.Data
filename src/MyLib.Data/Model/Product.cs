using MyLib.Data.EntityFramework;

namespace MyLib.Data.Model
{
    public class Product : Entity
    {
		public int IdProduct { get; set; }
		public int IdBrand { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
    }
}
