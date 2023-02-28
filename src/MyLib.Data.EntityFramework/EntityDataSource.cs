using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using MyLib.Extensions.XLinq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyLib.Data.EntityFramework
{
    public abstract class EntityDataSource<T>
        : DataAdapter where T : Entity, new()
    {

        public EntityDataSource(
            IDataBaseAdapter dataBase
            )
            : base(dataBase)
        {
        }

        public T Get(object key)
        {
            return GetEntity(GetQueryKey(key));
        }

        private QueryBase GetQueryKey(
            object keyValue
        ) =>
            QueryAdapter
            .CreateQueryGet(
                GetTableName()
                , GetPrimaryKey()
                , keyValue
            );

        public T Get(params (string Name, object Value)[] filterArr)
        {
            var lf = new ListFilter();
            filterArr
                .Select(t => new Filter(t.Name, t.Value))
                .Do(f => lf.AddRange(f));
            return Get(lf);
        }

        public T Get(
            string name
            , object value
        ) =>
            Get(new ListFilter(name, value));

        public T Get(
            ListFilter listFilter
        ) =>
            GetEntity(GetQueryFilter(listFilter));

        private QueryBase GetQueryFilter(
            ListFilter listFilter
            , OrderList orderList
        ) =>
            QueryAdapter
            .CreateQuerySelect(
                GetTableName()
                , listFilter
                , orderList
            );

        private QueryBase GetQueryFilter(
            ListFilter listFilter
        ) =>
            QueryAdapter
            .CreateQuerySelect(
                GetTableName()
                , listFilter
            );

        public IEnumerable<T> Select(
            object key
        ) =>
            GetEnumerable(GetQueryKey(key));

        public IEnumerable<T> Select(
            ListFilter listFilter
        ) =>
            GetEnumerable(
                GetQueryFilter(listFilter)
            );

        public IEnumerable<T> Select(
            ListFilter listFilter
            , OrderList orderList
        ) =>
            GetEnumerable(
                GetQueryFilter(
                    listFilter
                    , orderList
                )
            );

        public IEnumerable<T> SelectAll(
        ) =>
            GetEnumerable(
                QueryAdapter.CreateQuerySelect(
                    GetTableName()
                )
            );

        protected T GetEntity(
            QueryBase query
        )
        {
            var dr =
                QueryAdapter
                    .GetDataReader(
                        query
                    );

            T e = null;
            if (dr.Read())
            {
                e = GetEntity<T>(dr);
            }
            dr.Close();
            return e;
        }

        protected IEnumerable<T> GetEnumerable(
            QueryBase query
        ) =>
            GetEnumerable<T>(
                QueryAdapter.GetDataReader(query)
            );

        protected IEnumerable<T> GetEnumerable(
            IDataReader dr
            , bool close = true
        ) =>
            GetEnumerable<T>(dr, close);

        protected string GetTableName(
        ) =>
            (
                (Table)
                Attribute
                .GetCustomAttribute(
                    GetType()
                    , typeof(Table)
                )
            )
            ?.Name
            ?? GetType().Name;

        protected string GetPrimaryKey(
        ) =>
            GetProperties()
            .First(p => p.IsPrimaryKey)
            .Name;

        protected IEnumerable<string> GetPrimaryKeyFieldNames(
        ) =>
            GetProperties()
            .Where(p => p.IsPrimaryKey)
            .Select(p => p.Name);

        protected IEnumerable<PropertyField> GetProperties(
        ) =>
            typeof(T)
            .GetProperties()
            .Where(p => Attribute.IsDefined(p, typeof(Field)))
            .Select(p => new PropertyField(p))
            ;
    }
}
