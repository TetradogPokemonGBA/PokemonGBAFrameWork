using Gabriel.Cat.S.Binaris;
using Poke;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Habilidad
{
   public class Descripcion:IElementoBinarioComplejo
    {
        public const byte ID = 0x1D;
        public static readonly Zona ZonaDescripcionHabilidad;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Descripcion>();

        public BloqueString Texto { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        static Descripcion()
        {
            ZonaDescripcionHabilidad = new Zona("Zona descripcion habilidad");
            ZonaDescripcionHabilidad.Add(0x1C4, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
            ZonaDescripcionHabilidad.Add(EdicionPokemon.RubiUsa10, 0x9FE68, 0x9FE88);
            ZonaDescripcionHabilidad.Add(EdicionPokemon.ZafiroUsa10, 0x9FE68, 0x9FE88);
            ZonaDescripcionHabilidad.Add(0xA009C, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);

        }
        public static PokemonGBAFramework.Paquete GetDescripcion(RomGba rom)
        {

            return rom.GetPaquete("Descripciones Habilidades",(r,i)=>GetDescripcion(r,i),GetTotal(rom));
        }
        public static PokemonGBAFramework.Pokemon.DescripcionHabilidad GetDescripcion(RomGba rom,int index)
        {

            int offsetDescripcion = new OffsetRom(rom, Zona.GetOffsetRom(ZonaDescripcionHabilidad, rom).Offset + index * OffsetRom.LENGTH).Offset;

            return new PokemonGBAFramework.Pokemon.DescripcionHabilidad() { Descripcion = BloqueString.GetString(rom, offsetDescripcion).Texto };
        }
        public static int GetTotal(RomGba rom)
        {
            return 78;
        }
    }
}
