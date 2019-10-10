using Demo.Data.Tables.Rows;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Data.Tables
{
	public class Facturas : TableAdapter<Factura>
	{
		public Facturas(IDataBaseAdapter dataBase)
			: base(dataBase)
		{
		}

		public List<Factura> Select(DateTime fecha)
		{
			var dr =
				QueryAdapter
				.GetDataReader(
					new SqlQuery(
						@"
						SELECT * 
						FROM Facturas F 
						WHERE F.Fecha = @fecha
						;

						SELECT DF.* 
						FROM Facturas F 
							INNER JOIN DetalleFactura DF ON
								F.IdFactura = DF.IdFactura 
						WHERE F.Fecha = @fecha
						;
						"
						, new SqlParameterList { { "fecha", fecha } }
					)
				);

			var l = 
				GetEnumerable(dr, false)
				.ToList();

			dr.NextResult();

			var ld =
				GetEnumerable<DetalleFactura>(dr)
				.ToList();

			l.ForEach(
				f => 
					f.ListaDetalle = 
						ld.Where(
							d => d.IdFactura == f.IdFactura
						)
						.ToList()
			);

			ld = null;

			return l;
		}
	}
}
