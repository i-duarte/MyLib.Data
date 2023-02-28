namespace MyLib.Data.Common
{
    public class Filter
    {
        public Filter()
        { 
        }

        public Filter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}