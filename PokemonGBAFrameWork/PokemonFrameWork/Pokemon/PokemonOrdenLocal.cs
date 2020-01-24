using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
   public class OrdenLocal:PokemonFrameWorkItem
    {
        public const byte ID = 0x22;
        public static readonly Zona ZonaOrdenLocal;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<OrdenLocal>();

        public Word Orden { get; set; }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

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

        public static OrdenLocal GetOrdenLocal(RomGba rom, int posicion)
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
            if (((EdicionPokemon)rom.Edicion).RegionKanto)
                ordenLocal.IdFuente = EdicionPokemon.IDKANTO;
            else ordenLocal.IdFuente = EdicionPokemon.IDHOENN;

            ordenLocal.IdElemento = (ushort)posicion;
            return ordenLocal;
        }
        public static OrdenLocal[] GetOrdenLocal(RomGba rom)
        {
            OrdenLocal[] oredenesNacional = new OrdenLocal[Huella.GetTotal(rom)];
            for (int i = 0; i < oredenesNacional.Length; i++)
                oredenesNacional[i] = GetOrdenLocal(rom, i);
            return oredenesNacional;
        }
      }
}
