using System.Collections.Generic;

namespace MyLib.Data.Common
{
    public class ListFilter : List<Filter>
    {
        public ListFilter()
        { }

        public ListFilter(string name, object value)
        {
            Add(name, value);
        }

        public void Add(
            string name
            , object value
        )
        {
            Add(
                new Filter
                {
                    Name = name,
                    Value = value
                }
            );
        }
    }
}
