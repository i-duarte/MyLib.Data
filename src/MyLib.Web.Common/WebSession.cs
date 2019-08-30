using System;
using System.Web.SessionState;
using System.Web.UI;

namespace MyLib.Web.Common
{
	public class WebSession<T>
	{
		private string Nombre { get; set; }
		private HttpSessionState Session { get; set; }

		public WebSession(
			string nombre,
			HttpSessionState session
		)
		{
			Nombre = nombre;
			Session = session;
		}

		public WebSession(
			string nombre,
			Page page
		)
		{
			Nombre = nombre;
			Session = page.Session;
		}

		public T Valor
		{
			get =>
				string.IsNullOrEmpty(Convert.ToString(Session[Nombre]))
					? default(T)
					: (T)Session[Nombre];

			set => Session[Nombre] = value;
		}
	}
}
