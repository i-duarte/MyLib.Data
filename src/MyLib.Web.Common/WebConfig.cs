namespace MyLib.Web.Common
{
	public class WebConfig
	{
		protected static string GetAppSetting(string nombre)
			=> System
				.Configuration
				.ConfigurationManager
				.AppSettings[nombre];
	}
}
