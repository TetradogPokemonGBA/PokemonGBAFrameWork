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
        public static GranPaleta[] Get(RomGba rom, int offsetTablaPaletas,int total)
        {
            int offset = offsetTablaPaletas;
            GranPaleta[] paletas = new GranPaleta[total];

            for (int x = 0; x < total; x++)
            {
                paletas[x] = GranPaleta.Get(rom.Data.SubArray(new OffsetRom(rom, offset).Integer, GranPaleta.LENGTH*BasePaleta.LENGTHCOLOR));
                offset += OffsetRom.LENGTH;
            }
            return paletas;
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
