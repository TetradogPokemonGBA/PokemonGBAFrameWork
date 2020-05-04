using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PaletaNormal 
    {

        public PaletaNormal()
        {
            Paleta = new Paleta();
        }

        public Paleta Paleta { get; set; }


        public static PaletaNormal Get(RomGba rom, int posicion,OffsetRom offsetPaletaNormal=default)
        {
            if (Equals(offsetPaletaNormal, default))
                offsetPaletaNormal = GetOffset(rom);
            PaletaNormal paleta = new PaletaNormal();
            int offsetPaletaNormalPokemon =offsetPaletaNormal+ Paleta.LENGTHHEADERCOMPLETO * posicion;
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
        public static PaletaNormal[] Get(RomGba rom) => Huella.GetAll<PaletaNormal>(rom, PaletaNormal.Get, GetOffset(rom));

        public static PaletaNormal[] GetOrdenLocal(RomGba rom) => OrdenLocal.GetOrdenados<PaletaNormal>(rom, (r, o) => PaletaNormal.Get(r), GetOffset(rom));
        public static PaletaNormal[] GetOrdenNacional(RomGba rom) => OrdenNacional.GetOrdenados<PaletaNormal>(rom, (r, o) => PaletaNormal.Get(r), GetOffset(rom));
    }
}