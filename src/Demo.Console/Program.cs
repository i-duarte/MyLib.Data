using System;
using MyLib.Data;
using MyLib.Data.Model;

namespace Demo.DosConsole
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var db = 
				new DemoDataBase(
					"(local)\\dev2016"
					, "StockData"
				);

			Console.WriteLine(
				GetInfo(
					db
					.Products
					.GetItem(3)
				)
			);

			//db
			//	.Products
			//	.SelectAll()
			//	.Foreach(
			//		p =>
			//		Console
			//			.WriteLine(
			//				GetInfo(p)
			//			)
			//	);
		}

		private static string GetInfo(Product p)
		{
			return $"{p.Name} - {p.Description}";
		}
	}
}
