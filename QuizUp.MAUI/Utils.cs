using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUp.MAUI;
internal static class Utils
{
    public static int FindIndex<T>(this IList<T> list, Predicate<T> match, int startIndex = 0)
    {
        for (int i = startIndex; i < list.Count; i++)
        {
            if (match(list[i]))
            {
                return i;
            }
        }
        return -1;
    }
}
