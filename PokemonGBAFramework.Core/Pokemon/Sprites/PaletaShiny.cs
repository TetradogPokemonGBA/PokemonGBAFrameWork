using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PaletaShiny : BasePaletaSpritePokemon
    {

        public static readonly byte[] MuestraAlgoritmo = { 0x03, 0x48, 0xE9, 0x00, 0x09, 0x18 };
        public static readonly int IndexRelativo = 16 - MuestraAlgoritmo.Length;


        public static PaletaShiny Get(RomGba rom, int posicion, OffsetRom offsetImgFrontal = default)
        {
            return Get<PaletaShiny>(rom, posicion, offsetImgFrontal, MuestraAlgoritmo, IndexRelativo);
        }

        public static PaletaShiny[] Get(RomGba rom,OffsetRom offsetPaletaShiny=default)
        {
            return Get<PaletaShiny>(rom, MuestraAlgoritmo, IndexRelativo, offsetPaletaShiny);
        }

        public static PaletaShiny[] GetOrdenLocal(RomGba rom, OffsetRom offsetPaletaShiny = default)
        {
            return GetOrdenLocal<PaletaShiny>(rom, MuestraAlgoritmo, IndexRelativo, offsetPaletaShiny);
        }
        public static PaletaShiny[] GetOrdenNacional(RomGba rom, OffsetRom offsetPaletaShiny = default)
        {
            return GetOrdenNacional<PaletaShiny>(rom, MuestraAlgoritmo, IndexRelativo,offsetPaletaShiny);
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