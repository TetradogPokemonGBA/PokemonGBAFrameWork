using System;

namespace PokemonGBAFramework.Core
{
    public class PointerMalFormadoException : Exception
    {
        public PointerMalFormadoException() : base("Pointer mal formado") { }
    }
}