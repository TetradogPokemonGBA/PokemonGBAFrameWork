using Gabriel.Cat.S.Binaris;
using Poke;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
    public class OrdenNacional
    {
        public const byte ID = 0x23;
        public static readonly Zona ZonaOrdenNacional;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<OrdenNacional>();

        public Word Orden { get; set; }

        static OrdenNacional()
        {
            ZonaOrdenNacional = new Zona("Orden Nacional");

            //orden nacional
            ZonaOrdenNacional.Add(0x3FA08, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
            ZonaOrdenNacional.Add(0x3F83C, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
            ZonaOrdenNacional.Add(0x43128, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
            ZonaOrdenNacional.Add(EdicionPokemon.RojoFuegoUsa10, 0x4323C, 0x43250);
            ZonaOrdenNacional.Add(EdicionPokemon.VerdeHojaUsa10, 0x4323C, 0x43250);
            ZonaOrdenNacional.Add(0x6D448, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);


        }

        public static PokemonGBAFramework.Pokemon.OrdenLocal GetOrdenNacional(RomGba rom, int posicion)
        {
            OrdenLocal ordenLocal = new OrdenLocal();
            try
            {
                ordenLocal.Orden = new Word(rom, Zona.GetOffsetRom(ZonaOrdenNacional, rom).Offset + (posicion - 1) * 2);
            }
            catch
            {
                ordenLocal.Orden = null;
            }

            return new PokemonGBAFramework.Pokemon.OrdenLocal() { Orden = ordenLocal.Orden != null ? ordenLocal.Orden : -1 };
        }
        public static PokemonGBAFramework.Paquete GetOrdenNacional(RomGba rom)
        {
            return rom.GetPaquete("Ordenes Local", (r, i) => GetOrdenNacional(r, i), Huella.GetTotal(rom));
        }
    }
}
