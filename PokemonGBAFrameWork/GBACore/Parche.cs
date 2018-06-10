using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Binaris;
namespace PokemonGBAFrameWork
{

    public class Parche : IElementoBinarioComplejo
    {
        public class ParteAbsoluta : ParteRelativa
        {
            public static readonly new ElementoBinario Serializador = ElementoBinario.GetSerializador<ParteAbsoluta>();

            LlistaOrdenada<Edicion, byte[]> Off;

            protected override ElementoBinario ISerializador => Serializador;
        }
        public class ParteRelativa : IElementoBinarioComplejo
        {
            public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<ParteRelativa>();
            LlistaOrdenada<Edicion, LlistaOrdenada<OffsetRom, ParteRelativa>> PartesRelativas;
            LlistaOrdenada<Edicion, LlistaOrdenada<OffsetRom, ParteAbsoluta>> PartesAbsolutas;
            LlistaOrdenada<Edicion, LlistaOrdenada<Edicion, byte[]>> On;//los bytes tendrán posiblemente offsets a partesRelativas y partesAbsolutas

            protected virtual ElementoBinario ISerializador => Serializador;

            ElementoBinario IElementoBinarioComplejo.Serialitzer => ISerializador;

        }
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Parche>();

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;
    }
}
