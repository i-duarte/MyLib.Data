using System;
using Demo.Data;
using Demo.Data.Tables.Rows;
using MyLib.Data.SqlServer;
using MyLib.Data.PostgreSql;
using MyLib.Extensions.Linq;

namespace Demo.DosConsole
{
	internal class Program
	{
		private static void Main(
			string[] args
		)
		{
			TestSql3();
		}

		private static void TestPost()
		{
			var db =
				new StockData(
					new PostDataBaseAdapter(
						""
					)
				);
		}

		private static void TestSql2()
		{
			var db =
				new StockData(
					new SqlDataBaseAdapter(
						"(local)\\EXP2008"
						, "FacthorDiv"
					)
				);

			Console.WriteLine(
				"Demo master deatil"
			);

			var clientes = db.Clientes.Select(7, 201901, 201909);
		}

		private static void TestSql3()
		{
			var db =
				new StockData(
					new SqlDataBaseAdapter(
						"(local)\\DEV2016"
						, "BillerDataA3T"
					)
				);

			Console.WriteLine(
				"Demo master deatil"
			);

			var inicio = DateTime.Now;

			var facturas = db.Facturas.Select(new DateTime(2019, 9, 1));

			var fin = DateTime.Now;

			var diferencia = fin - inicio;
		}

		private static void TestSql1()
		{
			var db =
				new StockData(
					new SqlDataBaseAdapter(
						"(local)\\dev2016"
						, "StockData"
					)
				);

			Console.WriteLine(
				"Demo single read"
			);

			Console.WriteLine(
				GetInfo(
					db
						.Products
						.Get(3)
				)
			);

			Console.WriteLine(
				"Demo multiple read"
			);

			db
				.Products
				.SelectAll()
				.ForEach(
					p =>
					Console
						.WriteLine(
							GetInfo(p)
						)
				);
		}

		private static string GetInfo(
			Product p
		)
		{
			return $"{p.Name} - {p.Description}";
		}
	}
}
