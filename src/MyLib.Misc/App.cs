using System.IO;

namespace MyLib.Misc
{
    public static class App
    {
        public static string GetAppPath(
        ) => 
            Path.GetDirectoryName(
                System
                    .Reflection
                    .Assembly
                    .GetEntryAssembly()
                    ?.Location
                ?? System
                    .Reflection
                    .Assembly
                    .GetExecutingAssembly()
                    .Location
            )
            ;
    }
}
