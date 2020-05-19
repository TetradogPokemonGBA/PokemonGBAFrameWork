using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class BloqueBraille
    {
        public unsafe BloqueBraille(byte* ptrRom,int offset)
        {
            IdUnicoTemp = Script.GetIdUnicoTemp();
        }
        public int IdUnicoTemp { get; set; }
        public byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
