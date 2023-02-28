using System.Collections.Generic;

namespace MyLib.Data.Common
{
    public class OrderList : List<FieldOrder>
    {
        public void Add(
            string name
        ) => Add(name, true);

        public void Add(
            string name
            , bool ascending
        )
        {
            Add(
                new FieldOrder
                {
                    Name = name
                    ,
                    Ascending = ascending
                }
            );
        }
    }
}