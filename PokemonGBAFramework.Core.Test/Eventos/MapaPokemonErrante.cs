using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Eventos
{
    [TestClass]
   public class MapaPokemonErrante:BaseTestIndividual
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.PokemonErrante.Mapa>(romData, (r,o) => Core.PokemonErrante.Mapa.Get(r));
        }

    }
}
