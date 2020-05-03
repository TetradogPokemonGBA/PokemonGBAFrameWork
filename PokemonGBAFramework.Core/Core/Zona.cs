using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Extension;
namespace PokemonGBAFramework.Core
{
    public class Zona
    {
        public const int LENGTH = OffsetRom.LENGTH;
        public Zona(int offset)
        {
            Offset = offset;
        }
        public int Offset { get; set; }
        public override string ToString()
        {
            return Offset.ToString();
        }
        public static Zona Search(RomGba rom, byte[] muestraAlgoritmo, int offsetAlgoritmo = 0)
        {
            return Search(rom.Data.Bytes, muestraAlgoritmo, offsetAlgoritmo);
        }
        public static Zona Search(byte[] rom, byte[] muestraAlgoritmo, int offsetAlgoritmo = 0)
        {
            int busqueda = rom.SearchArray(muestraAlgoritmo);
            int offset = busqueda + muestraAlgoritmo.Length;
           
            offset += offsetAlgoritmo;

            if (busqueda==-1||!OffsetRom.Check(rom, offset))
                throw new Exception("La muestra o el offset fallan,porque no se ha encontrado un Pointer!!");

            return offset;
        }
        public static Zona SearchFirstOffset(RomGba rom, Zona zonaOffset)
        {
            return SearchFirstOffset(rom, zonaOffset);
        }
        public static Zona SearchFirstOffset(byte[] rom, Zona zonaOffset)
        {
            return Zona.Search(rom, rom.SubArray(zonaOffset, Zona.LENGTH));
        }

        #region conversion
        public static implicit operator Zona(int offset)
        {
            return offset >= 0 ? new Zona(offset) : default;
        }
        public static implicit operator int(Zona zona)
        {
            return zona.Offset;
        }
        #endregion
    }
}
