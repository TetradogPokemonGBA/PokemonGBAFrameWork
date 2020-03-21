using Gabriel.Cat.S.Binaris;
using Poke;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class PaletaShiny
    {
        public const byte ID = 0x26;
        public static readonly Zona ZonaPaletaShiny;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<PaletaShiny>();
        static PaletaShiny()
        {

            ZonaPaletaShiny = new Zona("Paleta Shiny");
            //Rubi y Zafiro USA tienen otras zonas
            ZonaPaletaShiny.Add(EdicionPokemon.RubiUsa10, 0x4098C, 0x409AC);
            ZonaPaletaShiny.Add(EdicionPokemon.ZafiroUsa10, 0x4098C, 0x409AC);
            //los demas todos iguales :)
            ZonaPaletaShiny.Add(0x134, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.VerdeHojaUsa10);

        }
        public PaletaShiny()
        {
            Paleta = new Paleta();
        }

        public Paleta Paleta { get; set; }

  
        public static PokemonGBAFramework.Pokemon.Sprites.PaletaShiny GetPaletaNormal(RomGba rom, int posicion)
        {
            PaletaShiny paleta = new PaletaShiny();
            int offsetPaletaNormalPokemon = Zona.GetOffsetRom(ZonaPaletaShiny, rom).Offset + Paleta.LENGTHHEADERCOMPLETO * posicion;
            paleta.Paleta = Paleta.GetPaleta(rom, offsetPaletaNormalPokemon);

            return new PokemonGBAFramework.Pokemon.Sprites.PaletaShiny() { Colores = paleta.Paleta.Colores };
        }
        public static PokemonGBAFramework.Paquete GetPaletaNormal(RomGba rom)
        {
            return rom.GetPaquete("Paletas normales Pokemon", (r, i) => GetPaletaNormal(r, i), Huella.GetTotal(rom));
        }
    }
}
