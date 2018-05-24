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
		private Field Attributes { get; }

		public bool IsPrimaryKey { get; }

		public string Name 
			=> string.IsNullOrEmpty(Attributes?.Name) 
			? Property.Name 
			: Attributes.Name;

		public int Size => Attributes.Size;
		public byte Precision => Attributes.Precision;
		public byte Scale => Attributes.Scale;

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

		public PropertyField(Entity entity, PropertyInfo property)
		{
			Entity = entity;
			Property = property;
			Attributes = 
				(Field)
				Attribute
					.GetCustomAttribute(
						property.GetType()
						, typeof(Field)
					);

			IsPrimaryKey = 
				Attribute
				.GetCustomAttribute(
					property.GetType()
					, typeof(PrimaryKey)
				) != null;
		}
	}
}
