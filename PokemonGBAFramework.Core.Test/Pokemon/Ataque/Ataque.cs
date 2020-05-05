using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Pokemon.Ataque
{
    [TestClass]
   public class Ataque : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.Ataque>(romData, (r, o) => Core.Ataque.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.Ataque[]>(romData, (r) => Core.Ataque.Get(r));
        }
    }
}
