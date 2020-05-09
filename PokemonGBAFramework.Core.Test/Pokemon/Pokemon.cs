using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Pokemon
{
    [TestClass]
    public class Pokemon : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.Pokemon>(romData, (r, o) => Core.Pokemon.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.Pokemon>(romData, (r) => Core.Pokemon.Get(r));
        }
    }
}
