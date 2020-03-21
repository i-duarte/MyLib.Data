using Demo.Data.Model.Entities;
using Demo.Data.Model.Interfaces;
using Demo.Data.Tables.Rows;
using MyLib.Data.Common;
using MyLib.Data.EntityFramework;
using MyLib.Data.SqlServer;
using MyLib.Extensions.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Demo.Data.Tables
{
	public class Clientes : EntityDataSource<Cliente>, IClientes<ICliente>
	{
		public Clientes(IDataBaseAdapter dataBase) 
			: base(dataBase)
		{
		}

		public void BulkInsert(
			IEnumerable<BulkCliente> listaClientes
		)
		{
			QueryAdapter.BulkCopy(listaClientes.ToDataTable(), "Test", 1);
		}

		public List<Cliente> Select(int idZona, int desde, int hasta)
		{
			var dr = 
				QueryAdapter.GetDataReader(
					new SqlQuery(
						@"
						SELECT C.* 
						FROM Clientes C 
						WHERE C.IdAreaResponsabilidad = @idZona 
							AND C.Estatus = 1
						;
						SELECT K.* 
						FROM Clientes C 
							INNER JOIN Consumos K ON
								C.IdCliente = K.IdCliente
									AND K.Mes >= @desde
									AND K.Mes <= @hasta
						WHERE C.IdAreaResponsabilidad = @idZona 
							AND C.Estatus = 1
						;
						", new SqlParameterList {
							{ "idZona", idZona }
							, { "desde", desde }
							, { "hasta", hasta }
						}
					)
				);

			var lista = GetEnumerable(dr, false).ToList();

			dr.NextResult();

			var sublista = GetEnumerable<Consumo>(dr).ToList();

			//foreach (var i in lista)
			//{
			//	i.Consumos = 
			//		sublista
			//		.Where(c => c.IdCliente == i.IdCliente)
			//		.ToList();
			//}

			sublista = null;

			return lista;

		}

		public IEnumerable<ICliente> SelectAllx()
		{
			return GetEnumerable(
				"SELECT * FROM Clientes"
			);
		}

		private IEnumerable<ICliente> GetEnumerable(string sql)
		{
			return GetEnumerable(new SqlQuery(sql));
		}

		IEnumerable<ICliente> IClientes<ICliente>.SelectAll()
		{
			throw new System.NotImplementedException();
		}
	}
}
