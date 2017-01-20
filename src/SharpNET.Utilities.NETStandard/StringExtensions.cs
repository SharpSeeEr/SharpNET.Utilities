using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpNET.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Inserts spaces between words in a camel cased string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FromCamelCase(this string text)
        {
            return Regex.Replace(text, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", @" $1"); //@"(\B[A-Z])"
        }

        public static string StripNonDigits(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            return new string(source.Where(char.IsDigit).ToArray());
        }

        public static string ToPhone(this string source)
        {
            if (string.IsNullOrEmpty(source)) return "";

            var number = source.StripNonDigits();
            long phone;
            if (!long.TryParse(number, out phone)) return source;
            if (number.Length == 10) return string.Format("{0:(###) ###-####}", phone);
            if (number.Length == 7) return string.Format("{0:###-####}", phone);
            if (number.Length > 10) return string.Format("{0:+### ## ###-###-####}", phone);
            return source;
        }
    }
}
