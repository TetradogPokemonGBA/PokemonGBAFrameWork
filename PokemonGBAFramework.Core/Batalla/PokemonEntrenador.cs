using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PokemonEntrenador 
    {
        public Word Especie { get; set; }

        public byte Ivs { get; set; }

        public Word Nivel { get; set; }

        public Word Item { get; set; }

        public Word Move1 { get; set; }

        public Word Move2 { get; set; }

        public Word Move3 { get; set; }

        public Word Move4 { get; set; }
     }
}