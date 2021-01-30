using System.Globalization;

namespace Carlton.Base.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToKebabCase(this string str)
        {
            str = char.ToLowerInvariant(str[0]) + str.Substring(1);
            var newStr = string.Empty;


            foreach(var chr in str)
            {
                if(char.IsUpper(chr))
                {
                    newStr += $"-{char.ToLower(chr, CultureInfo.InvariantCulture)}";
                }
                else
                {
                    newStr += chr;
                }
            }

            return newStr;
        }
    }
}

