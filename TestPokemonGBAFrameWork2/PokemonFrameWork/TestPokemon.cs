using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFrameWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPokemonGBAFrameWork2.PokemonFrameWork
{
    [TestClass]
   public class TestPokemon
    {
        [TestMethod]
        public void CargarPokemon()
        {
            RomPokemon rom = new RomPokemon(Properties.Resources.PokemonRojoFuego);
            PokemonCompleto.GetPokedex(rom);
        }
    }
}
