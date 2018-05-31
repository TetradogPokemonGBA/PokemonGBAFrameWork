using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Binaris;
namespace PokemonGBAFrameWork
{//        public const byte ID = 0x2D;proximo id
    public class ElementoSerializado: ElementoSinBase<byte, ushort, long>

    {
        public ElementoSerializado()
        {
        }

        public ElementoSerializado(byte idTipo, ushort idElemento, long id, IElementoBinarioComplejo elementoBase, IElementoBinarioComplejo elementoCompleto) : base(idTipo, idElemento, id, elementoBase, elementoCompleto)
        {
        }

        public ElementoSerializado(byte idTipo, ushort idElemento, long id, byte[] bytesBase, byte[] bytesCompletos) : base(idTipo, idElemento, id, bytesBase, bytesCompletos)
        {
        }


    }
}
