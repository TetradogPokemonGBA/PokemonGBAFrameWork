using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Batalla
{
    [TestClass]
    public class SpriteObjeto : BaseTest
    {
        public override void TestGetIndividual(byte[] romData)
        {
            base.TestGetIndividual<Core.SpriteObjeto>(romData, (r, o) => Core.SpriteObjeto.Get(r, o));
        }
        public override void TestGetTodos(byte[] romData)
        {
            base.TestGetTodos<Core.SpriteObjeto>(romData, (r) => Core.SpriteObjeto.Get(r));
        }
    }
}
