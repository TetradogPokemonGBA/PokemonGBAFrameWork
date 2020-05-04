using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class DescripcionHabilidad
    {
        public BloqueString Texto { get; set; }
        public static DescripcionHabilidad[] Get(RomGba rom)
        {
            DescripcionHabilidad[] descripcions = new DescripcionHabilidad[NombreHabilidad.GetTotal(rom)];
            for (int i = 0; i < descripcions.Length; i++)
                descripcions[i] = Get(rom, i);
            return descripcions;
        }
        public static DescripcionHabilidad Get(RomGba rom, int index,OffsetRom offsetDescripcionHabilidad=default)
        {
            if (Equals(offsetDescripcionHabilidad, default))
                offsetDescripcionHabilidad = GetOffset(rom);
            DescripcionHabilidad descripcion = new DescripcionHabilidad();
            int offsetDescripcion = new OffsetRom(rom, offsetDescripcionHabilidad + index * OffsetRom.LENGTH).Offset;
            descripcion.Texto.Texto = BloqueString.Get(rom, offsetDescripcion).Texto;
            return descripcion;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            throw new NotImplementedException();
        }
    }
}
