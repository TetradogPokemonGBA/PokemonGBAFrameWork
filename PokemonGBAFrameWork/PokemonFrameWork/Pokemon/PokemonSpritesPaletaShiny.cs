using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class PaletaShiny:PokemonFrameWorkItem
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

        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public static PaletaShiny GetPaletaShiny(RomGba rom, int posicion)
        {
            PaletaShiny paleta = new PaletaShiny();
            int offsetPaletaShinyPokemon = Zona.GetOffsetRom(ZonaPaletaShiny, rom).Offset + Paleta.LENGTHHEADERCOMPLETO * posicion;
            paleta.Paleta = Paleta.GetPaleta(rom, offsetPaletaShinyPokemon);
            paleta.IdFuente = EdicionPokemon.IDMINRESERVADO;
            paleta.IdElemento = (ushort)posicion;
            return paleta;
        }
        public static PaletaShiny[] GetPaletaShiny(RomGba rom)
        {
            PaletaShiny[] paletaShinys = new PaletaShiny[Huella.GetTotal(rom)];
            for (int i = 0; i < paletaShinys.Length; i++)
                paletaShinys[i] = GetPaletaShiny(rom, i);
            return paletaShinys;
        }
     }
}
