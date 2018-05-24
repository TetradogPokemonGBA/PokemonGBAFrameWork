using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Habilidad
{
   public class Descripcion
    {
        public static readonly Zona ZonaDescripcionHabilidad;

        public BloqueString Texto { get; set; }
        static Descripcion()
        {
            ZonaDescripcionHabilidad = new Zona("Zona descripcion habilidad");
            ZonaDescripcionHabilidad.Add(0x1C4, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);
            ZonaDescripcionHabilidad.Add(EdicionPokemon.RubiUsa, 0x9FE68, 0x9FE88);
            ZonaDescripcionHabilidad.Add(EdicionPokemon.ZafiroUsa, 0x9FE68, 0x9FE88);
            ZonaDescripcionHabilidad.Add(0xA009C, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

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
        public static void SetDescripcion(RomGba rom,int index,Descripcion descripcion)
        {
            int offsetDescripcion;
            try
            {
                offsetDescripcion = new OffsetRom(rom, Zona.GetOffsetRom(ZonaDescripcionHabilidad, rom).Offset + index * OffsetRom.LENGTH).Offset;
                BloqueString.Remove(rom, offsetDescripcion);
                BloqueString.SetString(rom, offsetDescripcion, descripcion.Texto);
            }
            catch
            {
                //quiere decir que no hay pointer y se tiene que poner todo
                rom.Data.SetArray(Zona.GetOffsetRom(ZonaDescripcionHabilidad, rom).Offset + index * OffsetRom.LENGTH, new OffsetRom(BloqueString.SetString(rom, descripcion.Texto)).BytesPointer);
            }
        }
        public static void SetDescripcion(RomGba rom,IList<Descripcion> descripciones)
        {
            OffsetRom offsetDescripcion;
            int totalActual = HabilidadCompleta.GetTotal(rom);

            if (totalActual != descripciones.Count)
            {
                offsetDescripcion = Zona.GetOffsetRom(ZonaDescripcionHabilidad, rom);
                //borro
                rom.Data.Remove(offsetDescripcion.Offset, totalActual * OffsetRom.LENGTH);
                if (totalActual < descripciones.Count)
                {
                    //reubico
                    OffsetRom.SetOffset(rom, offsetDescripcion, rom.Data.SearchEmptyBytes(descripciones.Count * OffsetRom.LENGTH));
                }
            }
            for (int i = 0; i < descripciones.Count; i++)
                SetDescripcion(rom, i, descripciones[i]);

        }
    }
}
