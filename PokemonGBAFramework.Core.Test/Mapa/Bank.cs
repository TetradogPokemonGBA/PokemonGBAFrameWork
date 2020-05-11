using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Mapa
{
    [TestClass]
    public class Bank:BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.Mapa.Basic.Bank>(romData, (r, o) => Core.Mapa.Basic.Bank.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.Mapa.Basic.Bank>(romData, (r) => Core.Mapa.Basic.Bank.Get(r));
        }
    }
}
