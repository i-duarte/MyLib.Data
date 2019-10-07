using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.EntityFramework.Attributes;
using System.Collections.Generic;

namespace Demo.Data.Tables.Rows
{
	public class Cliente : Entity
	{

		[Field(IsPrimaryKey = true)]
		public int IdCliente { get; set; }
		
		[Field]
		public string NumCuenta { get; set; }

		public List<Consumo> Consumos { get; set; }

		public Cliente() { }
		public Cliente(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}
	}
}
