using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class OrdenLocal:BaseOrden
    {

        public static readonly byte[] MuestraAlgoritmo = { 0x00, 0x0C, 0x01, 0x1C, 0xFA, 0x28, 0x10 };
        public static readonly int InicioRelativo = -MuestraAlgoritmo.Length - 48 - OffsetRom.LENGTH;

        public static OrdenLocal GetOrdenLocal(RomGba rom, int posicion,OffsetRom inicioOrdenLocal=default)
        {
            return BaseOrden.GetOrden<OrdenLocal>(rom, posicion, MuestraAlgoritmo, InicioRelativo, inicioOrdenLocal);
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static Zona GetZona(RomGba rom)
        {
            return Zona.Search(rom, MuestraAlgoritmo,InicioRelativo);
        }
        public static KeyValuePair<OrdenLocal,T> GetOrdenado<T>(RomGba rom,int posOriginal, GetMethod<T> metodo,OffsetRom offsetInicioMetodo=default,OffsetRom offsetInicioOrdenLocal=default)
        {
            return BaseOrden.GetOrdenado<OrdenLocal,T>(rom,MuestraAlgoritmo,InicioRelativo, posOriginal, metodo, offsetInicioMetodo, offsetInicioOrdenLocal);
        }
        public static T[] GetOrdenados<T>(RomGba rom,GetTodos<T> metodo,OffsetRom offsetMetodo=default,OffsetRom offsetInicioOrdenLocal=default)
        {
            return BaseOrden.GetOrdenados<OrdenLocal,T>(rom, MuestraAlgoritmo, InicioRelativo, metodo, offsetMetodo, offsetInicioOrdenLocal);
        }
    }
}