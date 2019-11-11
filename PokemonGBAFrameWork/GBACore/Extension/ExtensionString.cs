using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public static class StringExtension
    {
        public static bool StartsWith(this string str, params string[] toFindAny)
        {
            bool starts = false;
            for (int i = 0; i < toFindAny.Length && !starts; i++)
                starts = str.StartsWith(toFindAny[i]);
            return starts;
        }
    }
}
