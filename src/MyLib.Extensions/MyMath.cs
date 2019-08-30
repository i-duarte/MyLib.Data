using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Extensions
{
	public static class MyMath
	{
		public static decimal Round(this decimal v, int n) 
			=> Math.Round(v, n);
	}
}
