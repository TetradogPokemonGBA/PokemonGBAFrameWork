using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class ScriptXSEMalFormadoException:Exception
    {
        public ScriptXSEMalFormadoException(string linea):base($"Se ha encontrado un error en la linea '{linea}'") { }
    }
    public class ScriptRomMalFormadoException : Exception
    {
        public int Inicio { get; private set; }
        public int OffsetCercaDelProblema { get; private set; }
        public ScriptRomMalFormadoException(int inicio,int offsetCercaDelProblema) : base($"Se ha encontrado un error del Scprit({inicio}) cerca del offset '{offsetCercaDelProblema}'")
        {
            Inicio = inicio;
            OffsetCercaDelProblema = offsetCercaDelProblema;
        }
    }
}
