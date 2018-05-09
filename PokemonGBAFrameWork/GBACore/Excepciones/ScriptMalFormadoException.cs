using System;

namespace PokemonGBAFrameWork
{
    public class ScriptMalFormadoException : Exception
    {
        public ScriptMalFormadoException() : base("Script mal formado") { }
    }
}
