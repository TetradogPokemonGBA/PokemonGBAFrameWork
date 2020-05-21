using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class BloqueBraille
    {
        public static readonly string[] CaracteresNoPermitidos = { "\\l", "\\p", "\\n" };
        public BloqueBraille()
        {
            Texto = new BloqueString();
            IdUnicoTemp = Texto.IdUnicoTemp;
        }
        public unsafe BloqueBraille(byte* ptrRom,int offset)
        {
            Texto = BloqueString.Get(ptrRom, offset);
            IdUnicoTemp = Texto.IdUnicoTemp;
        }
        public int IdUnicoTemp { get; set; }
        public BloqueString Texto { get; set; }
        public byte[] GetBytes()
        {
            StringBuilder str = new StringBuilder(Texto.Texto);

            for (int i = 0; i < CaracteresNoPermitidos.Length; i++)
                str.Replace(CaracteresNoPermitidos[i], "");

            Texto.Texto = str.ToString();

            return BloqueString.ToByteArray(Texto.Texto);

        }
    }
}
