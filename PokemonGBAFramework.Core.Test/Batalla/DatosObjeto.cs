using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Batalla
{
    [TestClass]
    public class DatosObjeto:BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.DatosObjeto>(romData, (r, o) => Core.DatosObjeto.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.DatosObjeto>(romData, (r) => Core.DatosObjeto.Get(r));
        }
    }
}
