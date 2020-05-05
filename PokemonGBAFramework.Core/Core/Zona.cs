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
        public static Zona Search(RomGba rom, byte[] muestraAlgoritmo, int posicionPointer = 0)
        {
            return Search(rom.Data.Bytes, muestraAlgoritmo, posicionPointer);
        }
        public static Zona Search(byte[] rom, byte[] muestraAlgoritmo, int posicionPointer = 0)
        {
            int busqueda = rom.SearchArray(muestraAlgoritmo)+muestraAlgoritmo.Length;
            int offset = busqueda;

            if (busqueda == -1)
                throw new Exception("La muestra no se ha encontrado en la rom!");

            offset += posicionPointer;

            if (offset < 0)
                throw new Exception("No se ha encontrado ningun pointer, revisa el numero de pointers a saltar hasta la zona!");
            
            if (!OffsetRom.Check(rom, offset))
                throw new Exception("el offset no apunta a un Pointer!!");

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
