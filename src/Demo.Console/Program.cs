using System;
using Demo.Data;
using Demo.Data.Tables.Rows;
using MyLib.Data.SqlServer;
using MyLib.Extensions.Linq;

namespace Demo.DosConsole
{
	internal class Program
	{
		private static void Main(
			string[] args
		)
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
