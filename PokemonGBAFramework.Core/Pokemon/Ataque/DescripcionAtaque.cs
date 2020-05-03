using System;

namespace PokemonGBAFramework.Core
{
    public class DescripcionAtaque
    {
        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x80, 0x22, 0x08, 0xF0, 0x37, 0xFC };
        public static readonly int InicioRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 32;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x04, 0x22, 0x18, 0x68, 0x00, 0x19 };
        public static readonly int InicioRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length - 48;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0x00, 0xF0, 0x4A, 0xF8, 0x04, 0x48, 0x00 };
        public static readonly int InicioRelativoKanto = -MuestraAlgoritmoKanto.Length - 48;

        public BloqueString Texto { get; set; }
        public static int GetTotal(RomGba rom)
        {
            int total = 1;//como el primero no tiene me lo salto
            OffsetRom zonaDescripcionAtaque =new OffsetRom(rom, GetZona(rom));
            while (GetOffset(rom, zonaDescripcionAtaque, total).IsAPointer) total++;
            return total;
        }

        public static DescripcionAtaque Get(RomGba rom, int posicionAtaque,OffsetRom zonaDescripcionAtaque=default)
        {
            int offsetDescripcion;
            DescripcionAtaque descripcion = new DescripcionAtaque();
            if (posicionAtaque != 0)//el primero no tiene
            {
                offsetDescripcion = GetOffset(rom, zonaDescripcionAtaque, posicionAtaque).Offset;
                descripcion.Texto = BloqueString.GetString(rom, offsetDescripcion);

            }

            return descripcion;
        }
        public static DescripcionAtaque[] Get(RomGba rom) {
            DescripcionAtaque[] descripciones = new DescripcionAtaque[GetTotal(rom)];
            OffsetRom zonaDescrìpcionAtaque = GetZona(rom);
            for (int i = 0; i < descripciones.Length; i++)
                descripciones[i] = Get(rom, i, zonaDescrìpcionAtaque);
            return descripciones;
        }

        public static OffsetRom GetOffset(RomGba rom,OffsetRom zonaDescripcionAtaque=default,int posicionAtaque=1)
        {
            if (Equals(zonaDescripcionAtaque, default))
                zonaDescripcionAtaque =new OffsetRom(rom,GetZona(rom));
            return new OffsetRom(rom, zonaDescripcionAtaque+posicionAtaque*OffsetRom.LENGTH);
        }
        
        public static int GetZona(RomGba rom)
        {
            byte[] algoritmo;
            int inicio;
            if (rom.Edicion.EsEsmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;
                inicio = InicioRelativoEsmeralda;
            }else if (rom.Edicion.EsHoenn)
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;
                inicio = InicioRelativoRubiYZafiro;
            }else
            {
                algoritmo = MuestraAlgoritmoKanto;
                inicio = InicioRelativoKanto;
            }
            return Zona.Search(rom, algoritmo, inicio);
        }

        public static T[] GetAll<T>(RomGba rom,GetMethod<T> metodo,OffsetRom offsetMetodo)
        {
            T[] items = new T[GetTotal(rom)];
            for (int i = 0; i < items.Length; i++)
                items[i] = metodo(rom, i, offsetMetodo);
            return items;
        }
    }
}