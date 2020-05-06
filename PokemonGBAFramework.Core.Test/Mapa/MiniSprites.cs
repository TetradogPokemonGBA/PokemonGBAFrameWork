using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonGBAFramework.Core.Test.Batalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Mapa
{
    [TestClass]
    public class MiniSprites:BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.MiniSpriteMapa>(romData, (r, o) => Core.MiniSpriteMapa.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.MiniSpriteMapa[]>(romData, (r) => Core.MiniSpriteMapa.Get(r));
        }
    }
}
