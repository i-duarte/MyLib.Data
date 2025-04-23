namespace MyLib.Extensions
{
    public static class MyString
    {
        public static string AddCerosIzq(
            this string source
            , int longitud
        ) => source.PadLeft(longitud, '0')
            ;   

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
