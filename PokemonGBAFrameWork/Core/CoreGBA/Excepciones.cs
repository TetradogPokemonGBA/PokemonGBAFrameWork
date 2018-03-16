using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    /// <summary>
	/// Description of RomFaltaInvestigacionException.
	/// </summary>
	public class RomFaltaInvestigacionException : Exception
    {
        public RomFaltaInvestigacionException() : base("Falta investigación")
        {
        }
    }
    public class FormatoRomNoReconocidoException : Exception
    {
        public FormatoRomNoReconocidoException() : base("Formato no canonico")
        {
        }
    }
    public class RomSinEspacioException : Exception
    {
        public RomSinEspacioException() : base("Rom sin espacio")
        { }
    }
    public class ScriptMalFormadoException : Exception
    {
        public ScriptMalFormadoException() : base("Script mal formado") { }
    }
    public class PointerMalFormadoException : Exception
    {
        public PointerMalFormadoException() : base("Pointer mal formado") { }
    }
}
