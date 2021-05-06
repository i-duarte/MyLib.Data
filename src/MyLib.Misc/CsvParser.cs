using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyLib.Misc
{
    public static class CsvParser
	{
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
