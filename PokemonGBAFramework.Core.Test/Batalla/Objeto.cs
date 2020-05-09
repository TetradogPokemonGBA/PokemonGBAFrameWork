using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Batalla
{
    [TestClass]
    public class Objeto : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.Objeto>(romData, (r, o) => Core.Objeto.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.Objeto>(romData, (r) => Core.Objeto.Get(r));
        }
    }
}
