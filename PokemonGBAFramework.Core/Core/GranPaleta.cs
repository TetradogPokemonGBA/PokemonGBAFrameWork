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
        public static GranPaleta Get(RomGba rom, OffsetRom offsetTablaPaleta)
        {
            return Get(rom.Data.SubArray(offsetTablaPaleta, LENGTH * LENGTHCOLOR)); 
        }
        public static GranPaleta[] GetClone(params GranPaleta[] paletasOriginales)
        {
            GranPaleta[] paletas = new GranPaleta[paletasOriginales.Length];
            for (int i = 0; i < paletas.Length; i++)
                paletas[i] = (GranPaleta)paletasOriginales[i].Clon();
            return paletas;
        }
    }
}
