using Gabriel.Cat.S.Binaris;
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
        public static Descripcion[] GetDescripcion(RomGba rom)
        {
            Descripcion[] descripcions = new Descripcion[HabilidadCompleta.GetTotal(rom)];
            for (int i = 0; i < descripcions.Length; i++)
                descripcions[i] = GetDescripcion(rom, i);
            return descripcions;
        }
        public static Descripcion GetDescripcion(RomGba rom,int index)
        {
            Descripcion descripcion = new Descripcion();
            int offsetDescripcion = new OffsetRom(rom, Zona.GetOffsetRom(ZonaDescripcionHabilidad, rom).Offset + index * OffsetRom.LENGTH).Offset;
            descripcion.Texto.Texto= BloqueString.GetString(rom, offsetDescripcion).Texto;
            return descripcion;
        }
     }
}
