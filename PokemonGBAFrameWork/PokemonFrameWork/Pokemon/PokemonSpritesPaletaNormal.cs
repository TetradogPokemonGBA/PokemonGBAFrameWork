using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class PaletaNormal:IElementoBinarioComplejo
    {
        public const byte ID = 0x25;
        public static readonly Zona ZonaPaletaNormal;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<PaletaNormal>();
        static PaletaNormal()
        {
            ZonaPaletaNormal = new Zona("Paleta Normal");

            ZonaPaletaNormal.Add(EdicionPokemon.RubiUsa, 0x40954, 0x40974);
            ZonaPaletaNormal.Add(EdicionPokemon.ZafiroUsa, 0x40954, 0x40974);
            ZonaPaletaNormal.Add(0x130, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp, EdicionPokemon.EsmeraldaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.VerdeHojaUsa);

        }
        public PaletaNormal()
        {
            Paleta = new Paleta();
        }

        public Paleta Paleta { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        public static PaletaNormal GetPaletaNormal(RomGba rom, int posicion)
        {
            PaletaNormal paleta = new PaletaNormal();
            int offsetPaletaNormalPokemon = Zona.GetOffsetRom(ZonaPaletaNormal, rom).Offset + Paleta.LENGTHHEADERCOMPLETO * posicion;
            paleta.Paleta = Paleta.GetPaleta(rom, offsetPaletaNormalPokemon);
            return paleta;
        }
        public static PaletaNormal[] GetPaletaNormal(RomGba rom)
        {
            PaletaNormal[] paletaNormals = new PaletaNormal[Huella.GetTotal(rom)];
            for (int i = 0; i < paletaNormals.Length; i++)
                paletaNormals[i] = GetPaletaNormal(rom, i);
            return paletaNormals;
        }
        public static void SetPaletaNormal(RomGba rom, int posicion, PaletaNormal paleta)
        {
            paleta.Paleta.Id = (short)posicion;
            Paleta.SetPaleta(rom, paleta.Paleta);


        }
        public static void SetPaletaNormal(RomGba rom, IList<PaletaNormal> paletas)
        {
            //borro las paletas
            int total = Huella.GetTotal(rom);
            int offsetPaletaNormalPokemon = Zona.GetOffsetRom(ZonaPaletaNormal, rom).Offset;
            for (int i = 0; i < total; i++)
            {
                try
                {
                    Paleta.Remove(rom, offsetPaletaNormalPokemon);
                }
                catch { }
                rom.Data.Remove(offsetPaletaNormalPokemon, Paleta.LENGTHHEADERCOMPLETO);

                offsetPaletaNormalPokemon += Paleta.LENGTHHEADERCOMPLETO;
            }
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaPaletaNormal, rom), rom.Data.SearchEmptyBytes(paletas.Count * Paleta.LENGTHHEADERCOMPLETO));
            //pongo los datos
            for (int i = 0; i < paletas.Count; i++)
                SetPaletaNormal(rom, i, paletas[i]);
        }
    }
}
