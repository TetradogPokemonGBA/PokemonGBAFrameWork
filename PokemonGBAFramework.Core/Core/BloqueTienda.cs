using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class BloqueTienda
    {
        public unsafe BloqueTienda(byte* ptrRom,int offset)
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
