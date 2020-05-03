using System;

namespace PokemonGBAFramework.Core
{
    public class NombreAtaque : IComparable<NombreAtaque>
    {
        public const int LENGTH = 13;

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x20, 0x1C, 0x07, 0x21, 0xF5, 0xF0 };
        public static readonly int InicioRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 16;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x24, 0x18, 0x14, 0x4D, 0x00, 0x20, 0x28 };
        public static readonly int InicioRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length - 48;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0x10, 0xB5, 0x06, 0x4C, 0x06, 0x49 };
        public static readonly int InicioRelativoKanto = -MuestraAlgoritmoKanto.Length - OffsetRom.LENGTH;
        public NombreAtaque()
        {
            Texto = new BloqueString(LENGTH);
        }
        public BloqueString Texto { get; set; }

        public int CompareTo(NombreAtaque other)
        {
            int compareTo;
            if (other != default)
            {
                compareTo = Texto.CompareTo(other.Texto);
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        public override string ToString()
        {
            return Texto.ToString();
        }

        public static NombreAtaque Get(RomGba rom, int posicionAtaque, OffsetRom offsetInicioDescripcionAtaque = default)
        {
            if (Equals(offsetInicioDescripcionAtaque, default))
                offsetInicioDescripcionAtaque = GetOffset(rom);
            NombreAtaque nombre = new NombreAtaque();
            nombre.Texto = BloqueString.GetString(rom, offsetInicioDescripcionAtaque + posicionAtaque * LENGTH, LENGTH);
            return nombre;
        }
        public static NombreAtaque[] Get(RomGba rom) => DescripcionAtaque.GetAll<NombreAtaque>(rom, Get, GetOffset(rom));

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