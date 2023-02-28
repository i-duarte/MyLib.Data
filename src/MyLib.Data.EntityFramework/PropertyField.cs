using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace MyLib.Data.EntityFramework
{
    public class PropertyField : IField
    {
        private Entity Entity { get; }
        private PropertyInfo Property { get; }
        private Field FieldAttribute { get; }

        public string Name
            => string.IsNullOrEmpty(FieldAttribute?.Name)
            ? Property.Name
            : FieldAttribute.Name;

        public bool IsPrimaryKey => FieldAttribute.IsPrimaryKey;
        public bool IsIdentity => FieldAttribute.IsIdentity;
        public bool AllowNulls => FieldAttribute.AllowNulls;
        public bool ConvertValue => FieldAttribute.ConvertValue;
        public int Size => FieldAttribute.Size;
        public byte Precision => FieldAttribute.Precision;
        public byte Scale => FieldAttribute.Scale;
        public Type Type => Property.PropertyType;

        public object Value
        {
            get =>
                Property.GetValue(
                    Entity
                    , null
                );

            set =>
                Property.SetValue(
                    Entity
                    , value
                    , null
                );
        }

        public PropertyField(
            PropertyInfo property
        )
        {
            Property = property;

            var list =
                Attribute
                    .GetCustomAttributes(
                        property
                        , typeof(Field)
                    );

            if (list.Length > 0)
            {
                if (list.Length == 1)
                {
                    FieldAttribute = (Field)list[0];
                }
                else
                {
                    FieldAttribute =
                        (Field)
                        list
                        .First(
                            att =>
                            att.GetType().Name == nameof(Field)
                        );

                    if (FieldAttribute == null)
                    {
                        FieldAttribute = (Field)list[0];
                    }
                }
            }
        }

        public PropertyField(
            Entity entity
            , PropertyInfo property
        ) : this(property)
        {
            Entity = entity;
        }

        private static bool IsPresent(
            PropertyInfo property
            , Type t
        )
        {
            return
                Attribute
                .GetCustomAttribute(
                    property.GetType()
                    , t
                ) != null;
        }
    }
}

