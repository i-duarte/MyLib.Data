using System;
using Demo.Data;
using Demo.Data.Tables.Rows;
using MyLib.Extensions.Collection;

namespace Demo.DosConsole
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var db = 
				new StockData(
					"(local)\\dev2016"
					, "StockData"
				);

			Console.WriteLine("Demo single read");

			Console.WriteLine(
				GetInfo(
					db
					.Products
					.Select(3)
				)
			);

			Console.WriteLine("Demo multiple read");
			db
				.Products
				.SelectAll()
				.Foreach(
					p =>
					Console
						.WriteLine(
							GetInfo(p)
						)
				);
		}

		private static string GetInfo(Product p)
		{
			return $"{p.Name} - {p.Description}";
		}
	}
}
