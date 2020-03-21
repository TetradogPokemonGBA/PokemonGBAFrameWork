using Gabriel.Cat.S.Binaris;
using Poke;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Tipo
{
    public class Nombre
    {
        public enum LongitudCampo
        { Nombre = 7 }
        public const byte ID = 0x2B;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public static readonly Zona ZonaNombreTipo;

        public BloqueString Texto { get; set; }

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
        public static PokemonGBAFramework.Paquete GetNombre(RomGba rom)
        {
            return rom.GetPaquete("Nombre Tipos", (r,i)=>GetNombre(r,i), TipoCompleto.GetTotal(rom));
        }
        public static PokemonGBAFramework.Pokemon.NombrePokemon GetNombre(RomGba rom,int index)
        {
          return new PokemonGBAFramework.Pokemon.NombrePokemon() { Nombre = BloqueString.GetString(rom, Zona.GetOffsetRom(ZonaNombreTipo, rom, rom.Edicion).Offset + index * (int)LongitudCampo.Nombre, (int)LongitudCampo.Nombre, true).Texto };


        }
     
    }
}
