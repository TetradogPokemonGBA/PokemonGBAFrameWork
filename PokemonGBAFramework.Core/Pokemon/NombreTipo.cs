using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class NombreTipo
    {
        public const int LENGTH = 7;
        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0xBB, 0xFC, 0x00, 0x06, 0x01, 0x16 };
        public static readonly int InicioRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 16;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x71, 0xFE, 0x38, 0xBC };
        public static readonly int InicioRelativoRubiYZafiro = 12;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0xD1, 0x00, 0x89, 0x1A, 0x06 };
        public static readonly int InicioRelativoKanto = -MuestraAlgoritmoKanto.Length - 48;

        public BloqueString Texto { get; set; }
        public override string ToString()
        {
            return Texto.ToString();
        }
        public static int GetTotal(RomGba rom) =>18;
        public static NombreTipo Get(RomGba rom,int posicion,OffsetRom offsetInicioNombreTipo = default)
        {
            if (Equals(offsetInicioNombreTipo, default))
                offsetInicioNombreTipo = GetOffset(rom);
            NombreTipo nombre = new NombreTipo();
            nombre.Texto = BloqueString.Get(rom, offsetInicioNombreTipo + posicion * LENGTH);
            return nombre;
        }
        public static NombreTipo[] Get(RomGba rom,OffsetRom offsetNombreTipo=default)
        {
            NombreTipo[] nombres = new NombreTipo[GetTotal(rom)];
            OffsetRom offset =Equals(offsetNombreTipo,default)? GetOffset(rom):offsetNombreTipo;
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
