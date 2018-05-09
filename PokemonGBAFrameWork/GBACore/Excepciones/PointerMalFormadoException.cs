using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class PointerMalFormadoException : Exception
    {
        public PointerMalFormadoException() : base("Pointer mal formado") { }
    }
}
