using System;

namespace MyLib.Extensions
{
	public static class MyString
	{
		public static string JoinWith(
			this string source
			, string item
			, string separador
		) => 
			string.IsNullOrEmpty(source) 
			? item 
			: source + separador + item
			;
	}
}
