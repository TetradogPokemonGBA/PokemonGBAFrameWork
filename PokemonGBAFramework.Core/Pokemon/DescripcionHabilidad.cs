using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class DescripcionHabilidad
    {

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = {0x00, 0xF0, 0xA6, 0xFC, 0x03};
        public static readonly int IndexRelativoRubiYZafiro = 32 - MuestraAlgoritmoRubiYZafiro.Length;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0x17, 0xF8, 0x60, 0x1C, 0x00 };
        public static readonly int IndexRelativoKanto =  -MuestraAlgoritmoKanto.Length -16;

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0xB3, 0xFD, 0x11, 0x49, 0x00 };
        public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 16;
        public BloqueString Texto { get; set; }
        public static DescripcionHabilidad[] Get(RomGba rom)
        {
            DescripcionHabilidad[] descripcions = new DescripcionHabilidad[NombreHabilidad.GetTotal(rom)];
            OffsetRom offset = GetOffset(rom);
            for (int i = 0; i < descripcions.Length; i++)
                descripcions[i] = Get(rom, i,offset);
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

        public static Zona GetZona(RomGba rom)
        {
            byte[] algoritmo;
            int inicio;
            if (rom.Edicion.EsKanto)
            {
                algoritmo = MuestraAlgoritmoKanto;
                inicio = IndexRelativoKanto;
            }
            else if (rom.Edicion.Version == Edicion.Pokemon.Esmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;
                inicio = IndexRelativoEsmeralda;
            }
            else
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;
                inicio = IndexRelativoRubiYZafiro;
            }
            return Zona.Search(rom, algoritmo, inicio);
        }
    }
}
