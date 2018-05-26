using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
    public class OrdenNacional:IElementoBinarioComplejo
    {
        public const byte ID = 0x23;
        public static readonly Zona ZonaOrdenNacional;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<OrdenNacional>();

        public Word Orden { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

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

        public static OrdenNacional GetOrdenNacional(RomGba rom,int posicion)
        {
            OrdenNacional ordenNacional = new OrdenNacional();
            try
            {
                ordenNacional.Orden = new Word(rom, Zona.GetOffsetRom(ZonaOrdenNacional, rom).Offset + (posicion- 1) * 2);
            }
            catch {
                ordenNacional.Orden = null;
            }
            return ordenNacional;
        }
        public static OrdenNacional[] GetOrdenNacional(RomGba rom)
        {
            OrdenNacional[] oredenesNacional = new OrdenNacional[Huella.GetTotal(rom)];
            for (int i = 0; i < oredenesNacional.Length; i++)
                oredenesNacional[i] = GetOrdenNacional(rom, i);
            return oredenesNacional;
        }
        public static void SetOrdenNacional(RomGba rom,int posicion,OrdenNacional orden)
        {
            Word.SetData(rom, Zona.GetOffsetRom(ZonaOrdenNacional, rom).Offset + posicion * Word.LENGTH, orden.Orden==null?new Word(0):orden.Orden);

        }
        public static void SetOrdenNacional(RomGba rom,IList<OrdenNacional> ordenes)
        {
            rom.Data.Remove(Zona.GetOffsetRom(ZonaOrdenNacional, rom).Offset, Huella.GetTotal(rom) * Word.LENGTH);
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaOrdenNacional, rom), rom.Data.SearchEmptyBytes(ordenes.Count * Word.LENGTH));
            for (int i = 0; i < ordenes.Count; i++)
                SetOrdenNacional(rom, i, ordenes[i]);
        }
    }
}
