﻿using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Tipo
{
    public class Nombre:IElementoBinarioComplejo
    {
        public enum LongitudCampo
        { Nombre = 7 }
        public const byte ID = 0x2B;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public static readonly Zona ZonaNombreTipo;

        public BloqueString Texto { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        static Nombre()
        {
            ZonaNombreTipo = new Zona("Nombre Tipo");
            ZonaNombreTipo.Add(EdicionPokemon.ZafiroEsp10, 0x2E574);
            ZonaNombreTipo.Add(EdicionPokemon.RubiEsp10, 0x2E574);

            ZonaNombreTipo.Add(EdicionPokemon.ZafiroUsa10, 0x2E3A8);
            ZonaNombreTipo.Add(EdicionPokemon.RubiUsa10, 0x2E3A8);

            ZonaNombreTipo.Add(EdicionPokemon.EsmeraldaEsp10, 0x166F4);
            ZonaNombreTipo.Add(EdicionPokemon.EsmeraldaUsa10, 0x166F4);

            ZonaNombreTipo.Add(EdicionPokemon.RojoFuegoEsp10, 0x308B4);
            ZonaNombreTipo.Add(EdicionPokemon.VerdeHojaEsp10, 0x308B4);

            ZonaNombreTipo.Add(EdicionPokemon.RojoFuegoUsa10, 0x309C8, 0x309DC);
            ZonaNombreTipo.Add(EdicionPokemon.VerdeHojaUsa10, 0x309C8, 0x309DC);
        }
        public Nombre() : this("") { }
        public Nombre(string nombre)
        {
            Texto = new BloqueString((int)LongitudCampo.Nombre);
            Texto.Texto = nombre;
        }
        public static Nombre[] GetNombre(RomGba rom)
        {
            Nombre[] nombres = new Nombre[TipoCompleto.GetTotal(rom)];
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = GetNombre(rom, i);
            return nombres;
        }
        public static Nombre GetNombre(RomGba rom,int index)
        {
            Nombre nombre=new Nombre();
            nombre.Texto.Texto= BloqueString.GetString(rom, Zona.GetOffsetRom(ZonaNombreTipo, rom, rom.Edicion).Offset + index * (int)LongitudCampo.Nombre, (int)LongitudCampo.Nombre, true).Texto;
            return nombre;
        }
        public static void SetNombre(RomGba rom,int index,Nombre nombre)
        {
            BloqueString.SetString(rom, Zona.GetOffsetRom(ZonaNombreTipo, rom, rom.Edicion).Offset + index * (int)LongitudCampo.Nombre, nombre.Texto);
        }
        public static void SetNombre(RomGba rom,IList<Nombre> nombres)
        {
            int total = TipoCompleto.GetTotal(rom);
            rom.Data.Remove(Zona.GetOffsetRom(ZonaNombreTipo, rom, rom.Edicion).Offset, total * (int)LongitudCampo.Nombre);
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaNombreTipo, rom, rom.Edicion), rom.Data.SearchEmptyBytes(nombres.Count * (int)LongitudCampo.Nombre));
            for (int i = 0; i < nombres.Count; i++)
                SetNombre(rom, i, nombres[i]);
        }
    }
}