using System;

namespace PokemonGBAFramework.Core
{
    public class RomNoValidaException : Exception
    {
        public RomNoValidaException():base("Ha habido un error al leer la rom! mira que sea de Pokemon GBA Kanto o Hoenn") { }
    }
}
