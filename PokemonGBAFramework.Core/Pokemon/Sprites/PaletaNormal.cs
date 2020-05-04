using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PaletaNormal:BasePaleta 
    {

        public static readonly byte[] MuestraAlgoritmo = { 0x25, 0x1C, 0xCE, 0x20, 0x40, 0x00 };
        public static readonly int IndexRelativo = 16 - MuestraAlgoritmo.Length;

        public static PaletaNormal Get(RomGba rom, int posicion, OffsetRom offsetImgFrontal = default)
        {
            return Get<PaletaNormal>(rom, posicion, offsetImgFrontal, MuestraAlgoritmo, IndexRelativo);
        }

        public static PaletaNormal[] Get(RomGba rom, OffsetRom offsetPaletaNormal = default)
        {
            return Get<PaletaNormal>(rom, MuestraAlgoritmo, IndexRelativo, offsetPaletaNormal);
        }

        public static PaletaNormal[] GetOrdenLocal(RomGba rom, OffsetRom offsetPaletaNormal = default)
        {
            return GetOrdenLocal<PaletaNormal>(rom, MuestraAlgoritmo, IndexRelativo, offsetPaletaNormal);
        }
        public static PaletaNormal[] GetOrdenNacional(RomGba rom,OffsetRom offsetPaletaNormal=default)
        {
            return GetOrdenNacional<PaletaNormal>(rom, MuestraAlgoritmo, IndexRelativo, offsetPaletaNormal);
        }
        public static OffsetRom GetOffset(RomGba rom)
        {
            return GetOffset(rom, MuestraAlgoritmo, IndexRelativo);
        }
        public static Zona GetZona(RomGba rom)
        {
            return GetZona(rom, MuestraAlgoritmo, IndexRelativo);
        }



    }
}