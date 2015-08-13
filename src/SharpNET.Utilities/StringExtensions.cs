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
    }
}
