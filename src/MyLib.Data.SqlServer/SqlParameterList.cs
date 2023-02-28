using MyLib.Data.Common;
using System;
using System.Data;
using System.Data.SqlClient;


namespace MyLib.Data.SqlServer
{
    public class SqlParameterList
        : ParameterListBase
    {
        public SqlParameterList()
        {

        }

        public SqlParameterList(string name, object value)
        {
            Add(name, value);
        }

        public override void AddChar(
            string name
            , string value
        )
            => AddChar(
                name
                , value
                , value.Length
            );

        public override void AddChar(
            string name
            , string value
            , int size
        )
            => AddParameter(
                name
                , SqlDbType.Char
                , value
                , size
            );

        public override void Add(
            string name
            , byte value
        )
            => AddParameter(
                name
                , SqlDbType.TinyInt
                , value
            );

        public override void Add(
            string name
            , short value
            )
        {
            AddParameter(
                name
                , SqlDbType.SmallInt
                , value
            );
        }

        public override void Add(
            string name
            , int value
        )
            => AddParameter(
                name
                , SqlDbType.Int
                , value
            );

        public override void AddInt(
            string name
            , int? value
        )
        {
            if (value == null)
            {
                AddParameter(
                    name
                    , SqlDbType.Int
                );
            }
            else
            {
                Add(name, value.Value);
            }
        }

        public override void Add(
            string name
            , long value
        )
            => AddParameter(
                name
                , SqlDbType.BigInt
                , value
            );

        public override void Add(
            string name
            , float value
        )
            => AddParameter(
                name
                , SqlDbType.Real
                , value
            );

        public override void Add(
            string name
            , double value
        )
            => AddParameter(
                name
                , SqlDbType.Float
                , value
            );

        public override void Add(
            string name
            , bool value
        )
            => AddParameter(
                name
                , SqlDbType.Bit
                , value
            );

        public override void Add(
            string name
            , decimal value
        )
            => AddParameter(
                name
                , SqlDbType.Decimal
                , value
            );

        public override void Add(
            string name
            , decimal value
            , byte precision
            , byte scale
        )
            => AddParameter(
                name
                , SqlDbType.Decimal
                , value
                , precision
                , scale
            );

        public override void Add(
            string name
            , string value
        )
            => Add(name, value, value.Length);

        public override void Add(
            string name
            , string value
            , int size
        )
            => AddParameter(
                name
                , SqlDbType.VarChar
                , value
                , size
            );

        public override void Add(
            string name
            , DateTime value
        )
            => AddParameter(
                name
                , SqlDbType.DateTime
                , value
            );

        public override void Add(
            string name
            , TimeSpan value
        )
            => AddParameter(
                name
                , SqlDbType.Time
                , value
            );

        private void AddParameter(
            string name,
            SqlDbType type,
            object value
        )
        {
            var param =
                new SqlParameter(name, type)
                {
                    Direction = ParameterDirection.Input,
                    Value = value
                };
            Add(param);
        }

        private void AddParameter(
            string name
            , SqlDbType type
            , object value
            , byte presicion
            , byte scale
        )
        {
            var param =
                new SqlParameter(name, type)
                {
                    Direction = ParameterDirection.Input,
                    Precision = presicion,
                    Scale = scale,
                    Value = value
                };
            Add(param);
        }

        private void AddParameter(
            string name
            , SqlDbType type
            , object value
            , int size
        )
        {
            var param =
                new SqlParameter(name, type)
                {
                    Direction = ParameterDirection.Input,
                    Size = size,
                    Value = value
                };
            Add(param);
        }

        private void AddParameter(
            string name
            , SqlDbType type
        )
        {
            var param =
                new SqlParameter(name, type)
                {
                    Direction = ParameterDirection.Input
                };
            Add(param);
        }
    }
}
