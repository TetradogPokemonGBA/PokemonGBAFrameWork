using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Extension;
namespace PokemonGBAFramework.Core
{
    public class Zona
    {
        public const int ErrorAlgoritmoNoEncontrado = -404;
        public const int ErrorIndexRelativoNoApuntaAPointer = -303;
        public const int ErrorIndexNegativo = -202;

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
        public static Zona Search(RomGba rom, byte[] muestraAlgoritmo, int posicionPointer = 0, bool excepcionOCodigoError = true)
        {
            return Search(rom.Data.Bytes, muestraAlgoritmo, posicionPointer,excepcionOCodigoError);
        }
        public static Zona Search(byte[] rom, byte[] muestraAlgoritmo, int posicionPointer = 0,bool excepcionOCodigoError=true)
        {
            const int SINERROR = -1;
            int codigoError=SINERROR;
            int busqueda = rom.SearchArray(muestraAlgoritmo)+muestraAlgoritmo.Length;
            int offset = busqueda;

            if (busqueda == -1)
            {
                if (excepcionOCodigoError)
                    throw new Exception("La muestra no se ha encontrado en la rom!");
                else codigoError = ErrorAlgoritmoNoEncontrado;
            }
            if (codigoError == SINERROR)
            {
                offset += posicionPointer;

                if (offset < 0)
                {
                    if (excepcionOCodigoError)
                        throw new Exception("No se ha encontrado ningun pointer, revisa el IndexRelativo para posicionar bien!");
                    else codigoError = ErrorIndexNegativo;
                }
                if (codigoError == SINERROR && !OffsetRom.Check(rom, offset))
                {
                    if (excepcionOCodigoError)
                        throw new Exception("el offset no apunta a un Pointer!!");
                    else codigoError = ErrorIndexRelativoNoApuntaAPointer;
                }
            }
            return codigoError == SINERROR?offset:codigoError;
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
            return new Zona(offset);
        }
        public static implicit operator int(Zona zona)
        {
            return zona.Offset;
        }
        #endregion
    }
}
