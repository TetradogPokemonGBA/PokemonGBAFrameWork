using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Binaris;
namespace PokemonGBAFrameWork
{//        public const byte ID = 0x2C;proximo id
    public class SerializadorSimple<T> : ElementoSinBase<byte, ushort, ulong>
        where T:IElementoBinarioComplejo
    {
        public SerializadorSimple()
        {
        }

        public SerializadorSimple(byte idTipo, ushort idElemento, ulong id, IElementoBinarioComplejo elementoBase, IElementoBinarioComplejo elementoCompleto) : base(idTipo, idElemento, id, elementoBase, elementoCompleto)
        {
        }

        public SerializadorSimple(byte idTipo, ushort idElemento, ulong id, byte[] bytesBase, byte[] bytesCompletos) : base(idTipo, idElemento, id, bytesBase, bytesCompletos)
        {
        }

        public T GetElemento(T elementoBase)
        {
            return (T)elementoBase.Serialitzer.GetObject(GetBytesCompletos(elementoBase.Serialitzer.GetBytes(elementoBase)));
        }
    }
}
