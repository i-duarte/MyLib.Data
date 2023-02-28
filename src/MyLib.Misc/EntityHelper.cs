using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyLib.Misc
{
    public static class EntityHelper
    {
        
        //private static bool IsNotIdentity(
        //    PropertyInfo p
        //) =>
        //    !IsIdentity(p);

        //internal static long GetIdentityValue<T>(
        //    T entity
        //) where T : new()
        //{
        //    var identityField =
        //        GetPublicProperties(entity.GetType())
        //        .FirstOrDefault(IsIdentity);

        //    return identityField == null
        //        ? 0
        //        : Convert.ToInt64(
        //            identityField
        //            .GetValue(entity, null)
        //        );
        //}

        //private static bool IsIdentity(
        //    PropertyInfo p
        //) =>
        //    GetFieldAttribute(p)
        //        ?.IsIdentity
        //    ?? false;

        

        //public static string GetName(
        //    PropertyInfo property
        //) =>
        //    GetOrmAttribute(property)
        //    ?.Name
        //    ?? property.Name
        //    ;


        //public static string GetName(
        //    Type type
        //)
        //=> GetOrmAttribute(type)
        //    ?.Name
        //    ?? type.Name
        //    ;

        //public static OrmAttribute GetOrmAttribute(
        //    Type type
        //) =>
        //    (OrmAttribute)
        //    Attribute
        //    .GetCustomAttribute(
        //        type
        //        , typeof(OrmAttribute)
        //    )
        //    ;
        //public static OrmAttribute GetOrmAttribute(
        //    PropertyInfo property
        //) =>
        //    GetAttribute<OrmAttribute>(property);

        //public static FieldAttribute GetFieldAttribute(
        //    PropertyInfo property
        //) =>
        //    GetAttribute<FieldAttribute>(property);

        public static T GetAttribute<T>(
           PropertyInfo property
        ) where T : Attribute =>
            property
               .GetCustomAttributes(false)
               .FirstOrDefault(
                   a => a.GetType() == typeof(T)
               ) as T;

        //public static FieldAttribute GetFieldAttribute(
        //    Type type
        //)
        //{
        //    return ((FieldAttribute)
        //     Attribute
        //     .GetCustomAttribute(
        //         type
        //         , typeof(FieldAttribute)
        //     ))
        //     ;
        //}

        //public static TableAttribute GetTableAttribute(
        //    Type type
        //) =>
        //    (TableAttribute)
        //    Attribute
        //    .GetCustomAttribute(
        //        type
        //        , typeof(TableAttribute)
        //    )
        //    ;

        //public static string GetTableName(
        //    Type type
        //) =>
        //    GetTableAttribute(type)
        //    ?.Name
        //    ?? type.Name;


        public static T GetEntityCsv<T>(
            string[] values
            , List<string> columnNames
            , List<PropertyInfo> properties
        ) where T : new()
        {
            var t = new T();

            var pos = 0;

            foreach (var cn in columnNames)
            {
                var prop =
                properties
                .FirstOrDefault(
                    p =>
                    p.Name.ToUpper() == cn.ToUpper()
                );
                
                prop
                ?.SetValue(
                    t
                    , string.IsNullOrEmpty(values[pos])
                        ? null
                        : MyConvert(values[pos], prop)
                    , null
                );

                pos++;
            }
            return t;
        }

        private static object MyConvert(string v, PropertyInfo prop)
        {
            if(prop.PropertyType.BaseType.FullName == "System.Enum")
            {
                return Enum.Parse(prop.PropertyType, v);
            }

            switch(prop.PropertyType.FullName)
            {
                case "System.Int32":
                    return Convert.ToInt32(v);
                case "System.Int64":
                    return Convert.ToInt64(v);
                default:
                    return v;
            }            
        }

        public static List<PropertyInfo> GetPublicProperties(
            Type type
        )
        {
            return
                type
                .GetProperties()
                .Where(
                    p =>
                        !p.PropertyType.IsClass
                        || p.PropertyType == typeof(string)
                )
                .ToList()
                ;
        }

        public static int ToInt32(
            string i
            , int defaultValue = 0
        )
        {
            try
            {
                return int.Parse(i);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T Transform<T, TR>(TR obj)
        {
            throw new NotImplementedException();
        }
    }
}
