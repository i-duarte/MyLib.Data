using System;

namespace MyLib.Extensions
{
	public static class MyMath
	{
		public static decimal Round(this decimal v, int n) 
			=> Math.Round(v, n);
	}
}
