using System;

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
}
