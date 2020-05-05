using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Pokemon
{
    [TestClass]
    public class AtaquesAprendidos : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.AtaquesAprendidos>(romData, (r, o) => Core.AtaquesAprendidos.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.AtaquesAprendidos[]>(romData, (r) => Core.AtaquesAprendidos.Get(r));
        }
    }
}
