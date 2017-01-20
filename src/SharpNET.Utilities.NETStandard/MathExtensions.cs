using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities
{
    public static class MathExtensions
    {
        /// <summary>
        /// Calculates percentage <paramref name="num"/> is of <paramref name="total"/>
        /// </summary>
        /// <param name="num"></param>
        /// <param name="total"></param>
        /// <returns>Percentage rounded to whole number</returns>
        public static int Percent(int num, int total)
        {
            return (int)Math.Round((double)num / (double)total * 100);
        }
    }
}
