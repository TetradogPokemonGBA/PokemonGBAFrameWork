using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class PaletaShiny:IElementoBinarioComplejo
    {
        public static readonly Zona ZonaPaletaShiny;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<PaletaShiny>();
        static PaletaShiny()
        {

            ZonaPaletaShiny = new Zona("Paleta Shiny");
            //Rubi y Zafiro USA tienen otras zonas
            ZonaPaletaShiny.Add(EdicionPokemon.RubiUsa, 0x4098C, 0x409AC);
            ZonaPaletaShiny.Add(EdicionPokemon.ZafiroUsa, 0x4098C, 0x409AC);
            //los demas todos iguales :)
            ZonaPaletaShiny.Add(0x134, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp, EdicionPokemon.EsmeraldaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.VerdeHojaUsa);

        }
        public PaletaShiny()
        {
            Paleta = new Paleta();
        }

        public Paleta Paleta { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        public static PaletaShiny GetPaletaShiny(RomGba rom, int posicion)
        {
            PaletaShiny paleta = new PaletaShiny();
            int offsetPaletaShinyPokemon = Zona.GetOffsetRom(ZonaPaletaShiny, rom).Offset + Paleta.LENGTHHEADERCOMPLETO * posicion;
            paleta.Paleta = Paleta.GetPaleta(rom, offsetPaletaShinyPokemon);
            return paleta;
        }
        public static PaletaShiny[] GetPaletaShiny(RomGba rom)
        {
            PaletaShiny[] paletaShinys = new PaletaShiny[Huella.GetTotal(rom)];
            for (int i = 0; i < paletaShinys.Length; i++)
                paletaShinys[i] = GetPaletaShiny(rom, i);
            return paletaShinys;
        }
        public static void SetPaletaShiny(RomGba rom, int posicion, PaletaShiny paleta)
        {
            paleta.Paleta.Id = (short)posicion;
            Paleta.SetPaleta(rom, paleta.Paleta);


        }
        public static void SetPaletaShiny(RomGba rom, IList<PaletaShiny> paletas)
        {
            //borro las paletas
            int total = Huella.GetTotal(rom);
            int offsetPaletaShinyPokemon = Zona.GetOffsetRom(ZonaPaletaShiny, rom).Offset;
            for (int i = 0; i < total; i++)
            {
                try
                {
                    Paleta.Remove(rom, offsetPaletaShinyPokemon);
                }
                catch { }
                rom.Data.Remove(offsetPaletaShinyPokemon, Paleta.LENGTHHEADERCOMPLETO);

                offsetPaletaShinyPokemon += Paleta.LENGTHHEADERCOMPLETO;
            }
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaPaletaShiny, rom), rom.Data.SearchEmptyBytes(paletas.Count * Paleta.LENGTHHEADERCOMPLETO));
            //pongo los datos
            for (int i = 0; i < paletas.Count; i++)
                SetPaletaShiny(rom, i, paletas[i]);
        }
    }
}
