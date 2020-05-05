using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Pokemon
{
    [TestClass]
    public class NombreTipo : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.NombreTipo>(romData, (r, o) => Core.NombreTipo.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.NombreTipo[]>(romData, (r) => Core.NombreTipo.Get(r));
        }
    }
}
