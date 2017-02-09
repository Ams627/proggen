using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen
{
    static class Utility
    {
        /// <summary>
        /// Shifts a list like javascript shift
        /// </summary>
        /// <typeparam name="T">the thing that l is a list of</typeparam>
        /// <param name="l">the list</param>
        static void Shift<T>(ref List<T> l)
        {
            for (int i = 1; i < l.Count(); i++)
            {
                l[i - 1] = l[i];
            }
            l.Remove(l.Last());
        }
    }
}
