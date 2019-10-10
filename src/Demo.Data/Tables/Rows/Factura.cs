using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;
using System;
using System.Collections.Generic;

namespace Demo.Data.Tables.Rows
{
	public class Factura : Entity
	{

		[Field(IsPrimaryKey = true)]
		public int IdFactura { get; set; }

		[Field]
		public DateTime Fecha { get; set; }

		[Field]
		public DateTime Vigencia { get; set; }

		[Field]
		public decimal Subtotal { get; set; }

		[Field]
		public decimal Iva { get; set; }

		[Field]
		public decimal Total { get; set; }

		[Field]
		public bool AplicaCargoMinimo { get; set; }

		[Field]
		public bool AplicoDescuentoRastreo { get; set; }

		[Field]
		public int IdEstadoFactura { get; set; }

		[Field]
		public byte IdCategoriaFacturacion { get; set; }

		public List<DetalleFactura> ListaDetalle { get; set; }

		public Factura() { }
		public Factura(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
