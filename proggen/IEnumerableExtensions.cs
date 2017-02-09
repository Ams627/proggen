using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen
{
    static class IEnumerableExtensions
    {
        public static void ForEachExceptTheLast<T>(this IEnumerable<T> source,
                Action<T> usualAction,
                Action<T> lastAction
            )
        {
            var e = source.GetEnumerator();
            T penultimate;
            T last;
            if (e.MoveNext())
            {
                last = e.Current;
                while (e.MoveNext())
                {
                    penultimate = last;
                    last = e.Current;
                    usualAction(penultimate);
                }
                lastAction(last);
            }
        }
    }
}
