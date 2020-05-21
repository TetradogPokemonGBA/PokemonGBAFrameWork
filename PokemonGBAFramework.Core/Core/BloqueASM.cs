using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class BloqueASM
    {
        public unsafe BloqueASM(byte* ptrRom,int offset)
        {
            IdUnicoTemp = offset;//de momento
        }
        public int IdUnicoTemp { get; set; }
    }
}
