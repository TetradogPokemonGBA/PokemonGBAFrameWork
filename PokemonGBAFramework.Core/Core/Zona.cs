using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Extension;
namespace PokemonGBAFramework.Core
{
    public class Zona:Variable
    {

        public const int ErrorIndexRelativoNoApuntaAPointer = -303;
        public Zona() { }
        public Zona(int offset) => Offset = offset;
        public static new Zona Search(RomGba rom, byte[] muestraAlgoritmo, int indexRelativo, bool lanzarExcepcionOCodigo = false)
        {
            return Search(rom.Data.Bytes, muestraAlgoritmo, indexRelativo, lanzarExcepcionOCodigo);
        }
        public static new Zona Search(byte[] rom, byte[] muestraAlgoritmo, int indexRelativo, bool lanzarExcepcionOCodigo = false)
        {
            int zona = ISearch<Zona>(rom, muestraAlgoritmo, indexRelativo, lanzarExcepcionOCodigo);
            if (zona > 0)
            {
                if (!OffsetRom.Check(rom, zona))
                {
                    if (lanzarExcepcionOCodigo)
                        throw new Exception("el offset no apunta a un Pointer!!");
                    else zona = ErrorIndexRelativoNoApuntaAPointer;
                }
            }
            return zona;
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
