using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Batalla
{
    [TestClass]
    public class ClaseEntrenador:BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.ClaseEntrenador>(romData, (r, o) => Core.ClaseEntrenador.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.ClaseEntrenador[]>(romData, (r) => Core.ClaseEntrenador.Get(r));
        }
    }
}
