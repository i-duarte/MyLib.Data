using System.Collections.Generic;

namespace Demo.Data.Model.Entities
{
	public interface ICliente
	{
		int IdCliente { get; set; }
		string NumCuenta { get; set; }
		List<IConsumo> Consumos { get; }
	}
}
