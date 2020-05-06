using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class ScriptMalFormadoException:Exception
    {
        public ScriptMalFormadoException(int posicion):base($"Se ha encontrado un error en la posición {posicion}") { }
    }
}
