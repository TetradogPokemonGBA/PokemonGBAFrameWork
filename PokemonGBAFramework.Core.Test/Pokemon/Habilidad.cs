using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Pokemon
{
    [TestClass]
    public class Habilidad : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.Habilidad>(romData, (r, o) => Core.Habilidad.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.Habilidad>(romData, (r) => Core.Habilidad.Get(r));
        }
    }
}
