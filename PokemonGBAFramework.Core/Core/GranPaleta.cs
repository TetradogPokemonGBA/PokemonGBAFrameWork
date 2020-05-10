using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class GranPaleta : BasePaleta
    {
        public const int LENGTH= byte.MaxValue + 1;
        public GranPaleta() : base(LENGTH) { }
        public GranPaleta(params Color[] colores):this()
        {
            for (int i = 0; i < colores.Length;i++)
                Colores[i] = colores[i];
        }
        public override BasePaleta Clon()
        {
            return new GranPaleta(Colores);
        }
        public Paleta ToPaleta() => new Paleta(Colores);
        public static GranPaleta Get(byte[] datosDescomprimidos,int offset = 0)
        {
            return new GranPaleta(GetColors(datosDescomprimidos, LENGTH, offset));
        }
    }
}
