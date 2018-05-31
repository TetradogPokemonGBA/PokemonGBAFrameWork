using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class PaletaNormal:PokemonFrameWorkItem
    {
        public const byte ID = 0x25;
        public static readonly Zona ZonaPaletaNormal;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<PaletaNormal>();
        static PaletaNormal()
        {
            ZonaPaletaNormal = new Zona("Paleta Normal");

            ZonaPaletaNormal.Add(EdicionPokemon.RubiUsa10, 0x40954, 0x40974);
            ZonaPaletaNormal.Add(EdicionPokemon.ZafiroUsa10, 0x40954, 0x40974);
            ZonaPaletaNormal.Add(0x130, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.VerdeHojaUsa10);

        }
        public PaletaNormal()
        {
            Paleta = new Paleta();
        }

        public Paleta Paleta { get; set; }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public static PaletaNormal GetPaletaNormal(RomGba rom, int posicion)
        {
            PaletaNormal paleta = new PaletaNormal();
            int offsetPaletaNormalPokemon = Zona.GetOffsetRom(ZonaPaletaNormal, rom).Offset + Paleta.LENGTHHEADERCOMPLETO * posicion;
            paleta.Paleta = Paleta.GetPaleta(rom, offsetPaletaNormalPokemon);
            paleta.IdFuente = EdicionPokemon.IDMINRESERVADO;
            paleta.IdElemento = (ushort)posicion;
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
