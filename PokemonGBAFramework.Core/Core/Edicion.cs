using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Edicion
    {
        public enum Pokemon
        {
            Rubi,Zafiro,Esmeralda,RojoFuego,VerdeHoja
        }
        public Pokemon Version { get; set; }
        public bool EsKanto => Version >= Pokemon.RojoFuego;
        public bool EsHoenn => !EsKanto;
    }
}
