using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Test.Batalla
{
    public delegate T GetIndividual<T>(RomGba rom, int pos);
    public delegate T GetAll<T>(RomGba rom);

    [TestClass]
    public abstract class BaseTestIndividual
    {
        #region Roms Individual
        [TestMethod]
        public void TestGetIndividualRubiESP() => TestGetIndividual(Resource1.RubiESP);
        [TestMethod]
        public void TestGetIndividualZafiroITA() => TestGetIndividual(Resource1.ZafiroITA);
        [TestMethod]
        public void TestGetIndividualZafiroUSA12() => TestGetIndividual(Resource1.ZafiroUSA12);
        [TestMethod]
        public void TestGetIndividualEsmeraldaESP() => TestGetIndividual(Resource1.EsmeraldaESP);
        [TestMethod]
        public void TestGetIndividualEsmeraldaFRA() => TestGetIndividual(Resource1.EsmeraldaFRA);
        [TestMethod]
        public void TestGetIndividualEsmeraldaGER() => TestGetIndividual(Resource1.EsmeraldaGER);
        [TestMethod]
        public void TestGetIndividualEsmeraldaUSA() => TestGetIndividual(Resource1.EsmeraldaUSA);
        //[TestMethod]
        //public void TestGetIndividualEsmeraldaJAP() => TestGetIndividual(Resource1.EsmeraldaJAP);
        [TestMethod]
        public void TestGetIndividualVerdeHojaUSA11() => TestGetIndividual(Resource1.VerdeHojaUSA11);
        [TestMethod]
        public void TestGetIndividualRojoFuegoESP() => TestGetIndividual(Resource1.RojoFuegoESP);

        #endregion

        public abstract void TestGetIndividual(byte[] romData);

        protected void TestGetIndividual<T>(byte[] romData, GetIndividual<T> metodo)
        {
            RomGba rom = new RomGba(romData);
            Assert.IsNotNull(metodo(rom, 5));
        }
    }
    [TestClass]
    public abstract class BaseTest:BaseTestIndividual
    {

        #region Todos
        [TestMethod]
        public void TestGetTodosRubiESP() => TestGetTodos(Resource1.RubiESP);
        [TestMethod]
        public void TestGetTodosZafiroUSA12() => TestGetTodos(Resource1.ZafiroUSA12);
        [TestMethod]
        public void TestGetTodosZafiroITA() => TestGetTodos(Resource1.ZafiroITA);
        [TestMethod]
        public void TestGetTodosEsmeraldaESP() => TestGetTodos(Resource1.EsmeraldaESP);
        [TestMethod]
        public void TestGetTodosEsmeraldaFRA() => TestGetTodos(Resource1.EsmeraldaFRA);
        [TestMethod]
        public void TestGetTodosEsmeraldaGER() => TestGetTodos(Resource1.EsmeraldaGER);
        [TestMethod]
        public void TestGetTodosEsmeraldaUSA() => TestGetTodos(Resource1.EsmeraldaUSA);
        //[TestMethod]
        //public void TestGetTodosEsmeraldaJAP() => TestGetTodos(Resource1.EsmeraldaJAP);
        [TestMethod]
        public void TestGetTodosVerdeHojaUSA11() => TestGetTodos(Resource1.VerdeHojaUSA11);
        [TestMethod]
        public void TestGetTodosRojoFuegoESP() => TestGetTodos(Resource1.RojoFuegoESP);

        #endregion
        public abstract void TestGetTodos(byte[] romData);

        protected void TestGetTodos<T>(byte[] romData, GetAll<T> metodo)
        {
            RomGba rom = new RomGba(romData);
            Assert.IsNotNull(metodo(rom));
        }
    }
}



