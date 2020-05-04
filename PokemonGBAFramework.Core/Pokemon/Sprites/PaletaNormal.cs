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
            return Get<PaletaNormal>(rom, posicion, offsetImgFrontal, GetMuestra(rom), GetIndex(rom));
        }

        public static PaletaNormal[] Get(RomGba rom)
        {
            return Get<PaletaNormal>(rom, GetMuestra(rom), GetIndex(rom));
        }

        public static PaletaNormal[] GetOrdenLocal(RomGba rom)
        {
            return GetOrdenLocal<PaletaNormal>(rom, GetMuestra(rom), GetIndex(rom));
        }
        public static PaletaNormal[] GetOrdenNacional(RomGba rom)
        {
            return GetOrdenNacional<PaletaNormal>(rom, GetMuestra(rom), GetIndex(rom));
        }
        public static OffsetRom GetOffset(RomGba rom)
        {
            return GetOffset(rom, GetMuestra(rom), GetIndex(rom));
        }
        public static Zona GetZona(RomGba rom)
        {
            return GetZona(rom, GetMuestra(rom), GetIndex(rom));
        }
        static byte[] GetMuestra(RomGba rom)
        {
            return MuestraAlgoritmo;
        }

        static int GetIndex(RomGba rom)
        {
            return IndexRelativo;
        }
    }
}