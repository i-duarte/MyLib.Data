using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;
using System;
using System.Collections.Generic;

namespace Demo.Data.Tables.Rows
{
	public class DetalleFactura : Entity
	{

		[Field(IsPrimaryKey = true)]
		public int IdDetalleFactura { get; set; }

		[Field]
		public int IdFactura { get; set; }

		[Field]
		public int IdConceptoFactura { get; set; }

		[Field]
		public decimal Cargo { get; set; }

		[Field]
		public bool UsoInterno { get; set; }

		[Field]
		public bool UsoFiscal { get; set; }


		public DetalleFactura() { }
		public DetalleFactura(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
