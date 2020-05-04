using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PaletaShiny : BasePaleta
    {

        public static readonly byte[] MuestraAlgoritmo = { 0x03, 0x48, 0xE9, 0x00, 0x09, 0x18 };
        public static readonly int IndexRelativo = 16 - MuestraAlgoritmo.Length;


        public static PaletaShiny Get(RomGba rom, int posicion, OffsetRom offsetImgFrontal = default)
        {
            return Get<PaletaShiny>(rom, posicion, offsetImgFrontal, GetMuestra(rom), GetIndex(rom));
        }

        public static PaletaShiny[] Get(RomGba rom)
        {
            return Get<PaletaShiny>(rom, GetMuestra(rom), GetIndex(rom));
        }

        public static PaletaShiny[] GetOrdenLocal(RomGba rom)
        {
            return GetOrdenLocal<PaletaShiny>(rom, GetMuestra(rom), GetIndex(rom));
        }
        public static PaletaShiny[] GetOrdenNacional(RomGba rom)
        {
            return GetOrdenNacional<PaletaShiny>(rom, GetMuestra(rom), GetIndex(rom));
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