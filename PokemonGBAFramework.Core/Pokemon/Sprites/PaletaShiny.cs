using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PaletaShiny
    {

        public PaletaShiny()
        {
            Paleta = new Paleta();
        }

        public Paleta Paleta { get; set; }


        public static PaletaShiny Get(RomGba rom, int posicion, OffsetRom offsetPaletaNormal = default)
        {
            if (Equals(offsetPaletaNormal, default))
                offsetPaletaNormal = GetOffset(rom);
            PaletaShiny paleta = new PaletaShiny();
            int offsetPaletaNormalPokemon = offsetPaletaNormal + Paleta.LENGTHHEADERCOMPLETO * posicion;
            paleta.Paleta = Paleta.GetPaleta(rom, offsetPaletaNormalPokemon);
            return paleta;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            throw new NotImplementedException();
        }
        public static PaletaShiny[] Get(RomGba rom) => Huella.GetAll<PaletaShiny>(rom, PaletaShiny.Get, GetOffset(rom));

        public static PaletaShiny[] GetOrdenLocal(RomGba rom) => OrdenLocal.GetOrdenados<PaletaShiny>(rom, (r, o) => PaletaShiny.Get(r), GetOffset(rom));
        public static PaletaShiny[] GetOrdenNacional(RomGba rom) => OrdenNacional.GetOrdenados<PaletaShiny>(rom, (r, o) => PaletaShiny.Get(r), GetOffset(rom));
    }
}