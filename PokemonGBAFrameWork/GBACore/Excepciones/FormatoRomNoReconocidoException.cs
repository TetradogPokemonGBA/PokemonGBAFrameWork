using System;

namespace PokemonGBAFrameWork
{
    public class FormatoRomNoReconocidoException : Exception
    {
        public FormatoRomNoReconocidoException() : base("Formato no canonico")
        {
        }
    }
}
