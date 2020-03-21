using Demo.Data.Model.Entities;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;
using System.Collections.Generic;

namespace Demo.Data.Tables.Rows
{
	public class Cliente : Entity, ICliente
	{

		[Field(IsPrimaryKey = true)]
		public int IdCliente { get; set; }
		
		[Field]
		public string NumCuenta { get; set; }

		private List<IConsumo> _consumos;
		public List<IConsumo> Consumos 
			=> _consumos 
			?? (_consumos = new List<IConsumo>());

		public Cliente() { }
		public Cliente(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
