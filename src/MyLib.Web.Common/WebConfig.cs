using System;

namespace MyLib.Web.Common
{
    public static class WebConfig
    {
        public static int GetAppSettingInt(string nombre)
            => Convert.ToInt32(GetAppSetting(nombre));

        public static string GetAppSetting(string nombre)
            => System
                .Configuration
                .ConfigurationManager
                .AppSettings[nombre];


    }
}
