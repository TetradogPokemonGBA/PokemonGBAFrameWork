using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Habilidad
    {
        public NombreHabilidad Nombre { get; set; }
        public DescripcionHabilidad Descripcion { get; set; }

        public static Habilidad Get(RomGba rom,int index,OffsetRom offsetNombreHabilidad=default,OffsetRom offsetDescripcionHabilidad = default)
        {
            return new Habilidad() { Nombre = NombreHabilidad.Get(rom, index, offsetNombreHabilidad), Descripcion = DescripcionHabilidad.Get(rom, index, offsetDescripcionHabilidad) };
        }
        public static Habilidad[] Get(RomGba rom, OffsetRom offsetNombreHabilidad = default, OffsetRom offsetDescripcionHabilidad = default)
        {
            Habilidad[] habilidades;
            NombreHabilidad[] nombres = NombreHabilidad.Get(rom, offsetNombreHabilidad);
            DescripcionHabilidad[] descripciones = DescripcionHabilidad.Get(rom, offsetDescripcionHabilidad);
            habilidades = new Habilidad[descripciones.Length];
            for (int i = 0; i < habilidades.Length; i++)
                habilidades[i] = new Habilidad() { Nombre = nombres[i], Descripcion = descripciones[i] };
            return habilidades;
        }
    }
}
