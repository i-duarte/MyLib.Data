using System;

namespace Demo.Data.Model.Entities
{
	public interface IConsumo
	{
		int IdCliente { get; set; }
		int Mes { get; set; }
		DateTime FechaFacturacion { get; set; }
	}
}
