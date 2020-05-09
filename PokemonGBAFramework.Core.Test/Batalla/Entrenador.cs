using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Batalla
{
    [TestClass]
    public class Entrenador : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.Entrenador>(romData, (r, o) => Core.Entrenador.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.Entrenador>(romData, (r) => Core.Entrenador.Get(r));
        }
    }
}
