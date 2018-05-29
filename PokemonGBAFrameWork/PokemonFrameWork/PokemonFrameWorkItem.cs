using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public abstract class PokemonFrameWorkItem:IElementoBinarioComplejo
    {
        public byte IdTipo { get; set; }
        public ushort IdElemento { get; set; }
        public long IdFuente { get; set; }
        public abstract ElementoBinario Serialitzer { get; }
    }
}
