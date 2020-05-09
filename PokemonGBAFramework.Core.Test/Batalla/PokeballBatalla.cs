using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Batalla
{
    [TestClass]
    public class PokeballBatalla:BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.PokeballBatalla>(romData,(r,o)=> Core.PokeballBatalla.Get(r,o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.PokeballBatalla>(romData, (r) => Core.PokeballBatalla.Get(r));
        }
    }
}
