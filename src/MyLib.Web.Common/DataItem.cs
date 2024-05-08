namespace MyLib.Web.Common
{
    public class DataItem
    {
        public object Id { get; set; }
        public string Description { get; set; }

        public DataItem()
        {
        }

        public DataItem(object id, string descripcion)
        {
            Id = id;
            Description = descripcion;
        }
    }


}
