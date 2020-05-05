using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core.Test
{
    [TestClass]
    public class OffsetRom
    {
        /* C:\Users\tetra\Desktop\RomsPokemon USA and ESP\RomsPokemon USA and ESP\USA\Pokemon Emerald (U).gba (24/12/1996 21:32:00)
   Posición Inicial(h): 000D7490, Posición Final(h): 000D74EF, Longitud(h): 00000060 */

      static  byte[] Data = {
    0xA0, 0x39, 0x58, 0x08, 0x10, 0xB5, 0x00, 0x04, 0x00, 0x0C, 0x06, 0x4C,
    0xFF, 0xF7, 0xDE, 0xFF, 0x00, 0x04, 0x00, 0x0C, 0x2C, 0x21, 0x48, 0x43,
    0x00, 0x19, 0xC0, 0x89, 0x10, 0xBC, 0x02, 0xBC, 0x08, 0x47, 0x00, 0x00,
    0xA0, 0x39, 0x58, 0x08, 0x10, 0xB5, 0x00, 0x04, 0x00, 0x0C, 0x06, 0x4C,
    0xFF, 0xF7, 0xCC, 0xFF, 0x00, 0x04, 0x00, 0x0C, 0x2C, 0x21, 0x48, 0x43,
    0x00, 0x19, 0x00, 0x8A, 0x10, 0xBC, 0x02, 0xBC, 0x08, 0x47, 0x00, 0x00,
    0xA0, 0x39, 0x58, 0x08, 0x10, 0xB5, 0x00, 0x04, 0x00, 0x0C, 0x06, 0x4C,
    0xFF, 0xF7, 0xBA, 0xFF, 0x00, 0x04, 0x00, 0x0C, 0x2C, 0x21, 0x48, 0x43
};
        static int pointerCount=-1;

        static int PointerCount
        {
            get
            {
                if (Equals(pointerCount, -1))
                {
                    pointerCount = 0;
                    for (int i = 0; i < Data.Length; i++)
                        if (Equals((int)Data[i], 8))
                            pointerCount++;
                }
                return pointerCount;
            }
        }

        [TestMethod]
        public void TestSiguienteOffset()
        {
            int offset = Core.OffsetRom.GetOffsetPointerSiguiente(Data, 0);
            Assert.IsTrue(Core.OffsetRom.Check(Data, offset));
        }
        [TestMethod]
        public void TestAnteriorOffset()
        {
            int offset = Core.OffsetRom.GetOffsetPointerAnterior(Data, Data.Length-1);
            Assert.IsTrue(Core.OffsetRom.Check(Data, offset));
        }
        [TestMethod]
        public void TestSiguienteTodoOffset()
        {
            int offset=0;
            int total = 0;
            do
            {
                offset = Core.OffsetRom.GetOffsetPointerSiguiente(Data, offset);
                if (offset >= 0)
                    total++;
                offset+=Core.OffsetRom.LENGTH;
            } while (offset<Data.Length&&offset>0);
            Assert.AreEqual(PointerCount,total);
        }
        [TestMethod]
        public void TestAnteriorTodoOffset()
        {
            int offset = Data.Length-1;
            int total = 0;
            do
            {
                offset = Core.OffsetRom.GetOffsetPointerAnterior(Data, offset);
                if (offset >= 0)
                    total++;
            } while (offset > 0);
            Assert.AreEqual(PointerCount, total);
        }
    }
}
