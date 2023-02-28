namespace MyLib.Data.EntityFramework.Attributes
{
    public class NamedAttribute
        : System.Attribute
    {
        public string Name { get; set; }

        public NamedAttribute()
        {
        }

        public NamedAttribute(string name)
        {
            Name = name;
        }
    }
}
