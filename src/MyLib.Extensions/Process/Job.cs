using System;

namespace MyLib.Extensions.Process
{
	public static class Job
	{
		public static B Pipe<A, B>(this A x, Func<A, B> f) => f(x);

		public static void Do<T>(this T x, Action<T> f) => f(x);
	}
}
