using System.Collections.Generic;

namespace PokemonGBAFramework.Core
{
    public class OrdenNacional : BaseOrden
    {
        public static readonly byte[] MuestraAlgoritmo = { 0x04, 0x48, 0x81, 0x42, 0x0C, 0xD9, 0x04, 0x48 };
        public static readonly int InicioRelativo = -MuestraAlgoritmo.Length - 96;
        public static OrdenNacional[] Get(RomGba rom, OffsetRom offsetOrdenNacional = default, OffsetRom offsetHuella = default)
        {
            return BaseOrden.Get<OrdenNacional>(rom,MuestraAlgoritmo, InicioRelativo, offsetOrdenNacional,offsetHuella);
        }
        public static OrdenNacional Get(RomGba rom, int posicion, OffsetRom inicioOrdenNacional = default)
        {
            return BaseOrden.Get<OrdenNacional>(rom, posicion, MuestraAlgoritmo, InicioRelativo, inicioOrdenNacional);
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static Zona GetZona(RomGba rom)
        {
            return Zona.Search(rom, MuestraAlgoritmo, InicioRelativo);
        }
        public static KeyValuePair<OrdenNacional, T> GetOrdenado<T>(RomGba rom, int posOriginal, GetMethod<T> metodo, OffsetRom offsetInicioMetodo = default, OffsetRom offsetInicioOrdenNacional = default)
        {
            return BaseOrden.GetOrdenado<OrdenNacional, T>(rom, MuestraAlgoritmo, InicioRelativo, posOriginal, metodo, offsetInicioMetodo, offsetInicioOrdenNacional);
        }
        public static T[] GetOrdenados<T>(RomGba rom, GetTodos<T> metodo, OffsetRom offsetMetodo = default, OffsetRom offsetInicioOrdenNacional = default)
        {
            return BaseOrden.GetOrdenados<OrdenNacional, T>(rom, MuestraAlgoritmo, InicioRelativo, metodo, offsetMetodo, offsetInicioOrdenNacional);
        }
    } 
}