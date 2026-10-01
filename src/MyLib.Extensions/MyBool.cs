namespace MyLib.Extensions
{
    public static class MyBool
    {
        public static string ToStrSiNo(this bool source)
        {
            return source ? "Si" : "No";
        }
    }
}
