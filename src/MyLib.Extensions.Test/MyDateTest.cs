using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyLib.Extensions.Test
{
	[TestClass]
	public class MyDateTest
	{
		[TestMethod]
		public void ToSerialDate()
		{
			var f = new DateTime(1900, 1, 1);
			Assert.AreEqual(f.ToSerialDate(), 19000101);
		}

		[TestMethod]
		public void ToSerialDateTime()
		{
			var f = new DateTime(1900, 1, 1);
			Assert.AreEqual(f.ToSerialDateTime(), 19000101000000);
		}
	}
}
