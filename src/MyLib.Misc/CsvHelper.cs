using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyLib.Misc
{
    public static class CsvHelper
    {
        public static IEnumerable<T> Load<T>(string fileName) where T : new()
        {
            var lineas =
                File.ReadLines(fileName);

            var encabezado = 
                lineas.First().Split(',').ToList();

            foreach(var linea in lineas.Skip(1))
            {   
                var t = new T();
                
                var prop =
                    EntityHelper
                    .GetPublicProperties(t.GetType());

                yield return 
                    EntityHelper
                    .GetEntityCsv<T>(
                        Split(linea).ToArray()
                        , encabezado
                        , prop
                    );
            }
        }        

        public static string ToCsvStr(
            decimal d
        ) =>
            d.ToString("0.0000000000");

        public static string ToCsvStr(
            DateTime f
        ) =>
            f.ToString("yyyy/MM/dd HH:mm");

        public static string ToCsvStr(
            string s
        ) =>
            new StringBuilder()
                .Append("\"")
                .Append(s)
                .Append("\"")
                .ToString();

        public static string ToCsvStr(
            bool b
        ) =>
            b ? "1" : "0";

        public static IEnumerable<string> Split(string line)
        {
            var pattern =
                new Regex(
                    @"(?:^|,)(?=[^""]|("")?)""?((?(1)[^""]*|[^,""]*))""?(?=,|$)"
                    , RegexOptions.Multiline
                        | RegexOptions.IgnorePatternWhitespace
                        | RegexOptions.Singleline
                );
            Match matchResult = pattern.Match(line);
            while (matchResult.Success)
            {
                yield return matchResult.Groups[2].Value;
                matchResult = matchResult.NextMatch();
            }
        }
    }
}
