using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Variable
    {
        public const int ErrorAlgoritmoNoEncontrado = -404;
        public const int ErrorIndexNegativo = -202;

        public const int LENGTH = OffsetRom.LENGTH;
        public Variable() { }
        public Variable(int offset)
        {
            Offset = offset;
        }
        public int Offset { get; set; }
        public override string ToString()
        {
            return Offset.ToString();
        }

        public static Variable Search(RomGba rom,byte[] muestraAlgoritmo,int indexRelativo,bool excepticonOCodigoError = true)
        {
            return Search(rom.Data.Bytes, muestraAlgoritmo, indexRelativo, excepticonOCodigoError);
        }
        public static Variable Search(byte[] rom, byte[] muestraAlgoritmo, int indexRelativo, bool excepticonOCodigoError = true)
        {
            return ISearch<Variable>(rom, muestraAlgoritmo, indexRelativo, excepticonOCodigoError);
        }
        protected static T ISearch<T>(RomGba rom, byte[] muestraAlgoritmo, int posicionPointer = 0, bool excepcionOCodigoError = true) where T : Variable,new()
        {
            return ISearch<T>(rom.Data.Bytes, muestraAlgoritmo, posicionPointer, excepcionOCodigoError);
        }
        protected static T ISearch<T>(byte[] rom, byte[] muestraAlgoritmo, int posicionPointer = 0, bool excepcionOCodigoError = true) where T:Variable,new()
        {
            const int SINERROR = -1;
            int codigoError = SINERROR;
            int busqueda = rom.SearchArray(muestraAlgoritmo)  +muestraAlgoritmo.Length;
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

            }
            return new T() { Offset = codigoError == SINERROR ? offset : codigoError };
        }
        protected static T SearchFirstOffset<T>(RomGba rom, T zonaOffset) where T : Variable, new()
        {
            return SearchFirstOffset<T>(rom, zonaOffset);
        }
        protected static T SearchFirstOffset<T>(byte[] rom, T zonaOffset) where T:Variable,new()
        {
            return ISearch<T>(rom, rom.SubArray(zonaOffset, LENGTH));
        }

        #region conversion
        public static implicit operator Variable(int offset)
        {
            return new Variable(offset);
        }
        public static implicit operator int(Variable zona)
        {
            return zona.Offset;
        }
        #endregion
    }
}
