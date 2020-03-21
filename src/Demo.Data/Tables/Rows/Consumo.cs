using Demo.Data.Model.Entities;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;
using System;

namespace Demo.Data.Tables.Rows
{
	public class Consumo : Entity, IConsumo
	{
		[Field]
		public int IdCliente { get; set; }

		[Field]
		public int Mes { get; set; }

		[Field]
		public DateTime FechaFacturacion { get; set; }

		public Consumo() { }

		public Consumo(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
