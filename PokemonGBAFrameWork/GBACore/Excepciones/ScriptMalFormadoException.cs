using System;

namespace PokemonGBAFrameWork
{
    public class ScriptMalFormadoException : Exception
    {
        public ScriptMalFormadoException() : base("Script mal formado") { }
        public ScriptMalFormadoException(int linea):base(String.Format("Script mal formado en linea {0}",linea))
        { }
        public ScriptMalFormadoException(OffsetRom offset) : base(String.Format("Script mal formado en offset {0}", offset))
        { }
    }
}
