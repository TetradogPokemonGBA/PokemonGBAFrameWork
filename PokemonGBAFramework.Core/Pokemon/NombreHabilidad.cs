using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class NombreHabilidad
    {
        public const int LENGTH = 13;
        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x64, 0x18, 0x21, 0x68, 0x00 };
        public static readonly int InicioRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 48;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = {0x04, 0x21, 0x12, 0x22, 0x05
};
        public static readonly int InicioRelativoRubiYZafiro = -MuestraAlgoritmoEsmeralda.Length -32;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0x03, 0x2C, 0xF7, 0xD9, 0x06 };
        public static readonly int InicioRelativoKanto = -MuestraAlgoritmoKanto.Length - 32;
        public BloqueString Texto { get; set; }
        public override string ToString()
        {
            return Texto.ToString();
        }
        public static int GetTotal(RomGba rom) => 78;
        public static NombreHabilidad Get(RomGba rom, int posicion, OffsetRom offsetInicioNombreTipo = default)
        {
            if (Equals(offsetInicioNombreTipo, default))
                offsetInicioNombreTipo = GetOffset(rom);
            NombreHabilidad nombre = new NombreHabilidad();
            nombre.Texto = BloqueString.Get(rom, offsetInicioNombreTipo + posicion * LENGTH,LENGTH);
            return nombre;
        }
        public static NombreHabilidad[] Get(RomGba rom)
        {
            NombreHabilidad[] nombres = new NombreHabilidad[GetTotal(rom)];
            OffsetRom offset = GetOffset(rom);
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = Get(rom, i, offset);
            return nombres;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }
        public static int GetZona(RomGba rom)
        {
            byte[] algoritmo;
            int inicio;
            if (rom.Edicion.EsEsmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;
                inicio = InicioRelativoEsmeralda;
            }
            else if (rom.Edicion.EsHoenn)
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;
                inicio = InicioRelativoRubiYZafiro;
            }
            else
            {
                algoritmo = MuestraAlgoritmoKanto;
                inicio = InicioRelativoKanto;
            }
            return Zona.Search(rom, algoritmo, inicio);
        }
    }
}
