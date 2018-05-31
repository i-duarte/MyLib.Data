using System;
using System.Reflection;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework.Attributes;

namespace MyLib.Data.EntityFramework
{
	public class PropertyField : IField
	{
		private Entity Entity { get; }
		private PropertyInfo Property { get; }
		private Field FieldAttribute { get; }

		public bool IsPrimaryKey { get; }
		public bool IsIdentity { get; }

		public string Name 
			=> string.IsNullOrEmpty(FieldAttribute?.Name) 
			? Property.Name 
			: FieldAttribute.Name;

		public int Size => FieldAttribute.Size;
		public byte Precision => FieldAttribute.Precision;
		public byte Scale => FieldAttribute.Scale;

		public object Value
		{
			get => 
				Property.GetValue(
					Property
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
			Entity entity
			, PropertyInfo property
		)
		{
			Entity = entity;
			Property = property;
			FieldAttribute = 
				(Field)
				Attribute
					.GetCustomAttribute(
						property.GetType()
						, typeof(Field)
					);

			IsPrimaryKey = 
				IsPresent(
					property
					, typeof(PrimaryKey)
				);

			IsIdentity = 
				IsPresent(
					property
					, typeof(Identity)
				);
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

