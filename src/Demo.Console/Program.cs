using MyLib.Data;
using MyLib.Misc;

namespace Demo.Console
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var demoDatabase = new DemoDataBase("(local)\\dev2016", "StockData");
			foreach (var x in demoDatabase.Products.Select())
			{
				System.Console.WriteLine(x.Name);
			}

			demoDatabase
				.Products
				.Select()
				.Foreach(
					p => System.Console.WriteLine(p.Name)
				);
		}
	}
}
