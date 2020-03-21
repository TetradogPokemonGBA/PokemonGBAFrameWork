using Gabriel.Cat.S.Binaris;
using Poke;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
   public class OrdenLocal
    {
        public const byte ID = 0x22;
        public static readonly Zona ZonaOrdenLocal;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<OrdenLocal>();

        public Word Orden { get; set; }


        static OrdenLocal()
        {
            ZonaOrdenLocal = new Zona("Orden Local");
            //orden local
            ZonaOrdenLocal.Add(0x3F9BC, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
            ZonaOrdenLocal.Add(0x3F7F0, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
            ZonaOrdenLocal.Add(0x430DC, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
            ZonaOrdenLocal.Add(EdicionPokemon.RojoFuegoUsa10, 0x431F0, 0x43204);
            ZonaOrdenLocal.Add(EdicionPokemon.VerdeHojaUsa10, 0x431F0, 0x43204);
            ZonaOrdenLocal.Add(0x6D3FC, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);

        }

        public static PokemonGBAFramework.Pokemon.OrdenLocal GetOrdenLocal(RomGba rom, int posicion)
        {
            OrdenLocal ordenLocal = new OrdenLocal();
            try
            {
                ordenLocal.Orden = new Word(rom, Zona.GetOffsetRom(ZonaOrdenLocal, rom).Offset + (posicion - 1) * 2);
            }
            catch
            {
                ordenLocal.Orden = null;
            }

            return new PokemonGBAFramework.Pokemon.OrdenLocal() { Orden = ordenLocal.Orden != null ? ordenLocal.Orden : -1 };
        }
        public static PokemonGBAFramework.Paquete GetOrdenLocal(RomGba rom)
        {
            return rom.GetPaquete("Ordenes Local", (r, i) => GetOrdenLocal(r, i), Huella.GetTotal(rom));
        }
      }
}
