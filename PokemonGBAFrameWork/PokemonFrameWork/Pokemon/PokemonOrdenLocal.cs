using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
   public class OrdenLocal:IElementoBinarioComplejo
    {
        public static readonly Zona ZonaOrdenLocal;
        public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(OrdenLocal));

        public Word Orden { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        static OrdenLocal()
        {
            ZonaOrdenLocal = new Zona("Orden Local");
            //orden local
            ZonaOrdenLocal.Add(0x3F9BC, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);
            ZonaOrdenLocal.Add(0x3F7F0, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
            ZonaOrdenLocal.Add(0x430DC, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
            ZonaOrdenLocal.Add(EdicionPokemon.RojoFuegoUsa, 0x431F0, 0x43204);
            ZonaOrdenLocal.Add(EdicionPokemon.VerdeHojaUsa, 0x431F0, 0x43204);
            ZonaOrdenLocal.Add(0x6D3FC, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);

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
            return ordenLocal;
        }
        public static OrdenLocal[] GetOrdenLocal(RomGba rom)
        {
            OrdenLocal[] oredenesNacional = new OrdenLocal[Huella.GetTotal(rom)];
            for (int i = 0; i < oredenesNacional.Length; i++)
                oredenesNacional[i] = GetOrdenLocal(rom, i);
            return oredenesNacional;
        }
        public static void SetOrdenLocal(RomGba rom, int posicion, OrdenLocal orden)
        {
            Word.SetData(rom, Zona.GetOffsetRom(ZonaOrdenLocal, rom).Offset + posicion * Word.LENGTH, orden.Orden == null ? new Word(0) : orden.Orden);

        }
        public static void SetOrdenLocal(RomGba rom, IList<OrdenLocal> ordenes)
        {
            rom.Data.Remove(Zona.GetOffsetRom(ZonaOrdenLocal, rom).Offset, Huella.GetTotal(rom) * Word.LENGTH);
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaOrdenLocal, rom), rom.Data.SearchEmptyBytes(ordenes.Count * Word.LENGTH));
            for (int i = 0; i < ordenes.Count; i++)
                SetOrdenLocal(rom, i, ordenes[i]);
        }
    }
}
