﻿using Gabriel.Cat.S.Binaris;
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
     }
}
