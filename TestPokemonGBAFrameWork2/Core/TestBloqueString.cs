using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
using PokemonGBAFrameWork;

namespace TestPokemonGBAFrameWork2
{
    [TestClass]
   public class TestBloqueString
    {
        [TestMethod]
        public  void TestBloqueStringLetrasSimplesMinusculas()
        {
            const char INICIO = 'a';
            const char FIN = 'z';
            string testMinusculas = TestString(INICIO, FIN);
            byte[] stringToGBA = BloqueString.ToByteArray(testMinusculas);
            string GBAToString = BloqueString.ToString(stringToGBA);

            Assert.AreEqual(testMinusculas, GBAToString);
            
        }
        [TestMethod]
        public  void TestBloqueStringLetrasSimplesMayusculas()
        {
            const char INICIO = 'A';
            const char FIN = 'Z';
            string testMinusculas = TestString(INICIO, FIN);
            byte[] stringToGBA = BloqueString.ToByteArray(testMinusculas);
            string GBAToString = BloqueString.ToString(stringToGBA);

            Assert.AreEqual(testMinusculas, GBAToString);

        }
        [TestMethod]
        public  void TestBloqueStringNumeros()
        {
            const char INICIO = '0';
            const char FIN = '9';
            string testMinusculas = TestString(INICIO, FIN);
            byte[] stringToGBA = BloqueString.ToByteArray(testMinusculas);
            string GBAToString = BloqueString.ToString(stringToGBA);

            Assert.AreEqual(testMinusculas, GBAToString);

        }
        static string TestString(char inicio,char fin)
        {
            StringBuilder strTestMinusculas = new StringBuilder();
            for (char i = inicio; i <= fin; i++)
                strTestMinusculas.Append(i);
            return strTestMinusculas.ToString();
        }
        [TestMethod]
        public  void TestBloqueBytes()
        {

            byte[] origen = new byte[byte.MaxValue + 1];
            string GBAToString; 
            byte[] stringToGBA;

            for (int i = byte.MinValue; i <= byte.MaxValue; i++)
                origen[i] = (byte)i;

            GBAToString = BloqueString.ToString(origen);
            stringToGBA = BloqueString.ToByteArray(GBAToString);

            Assert.IsTrue(origen.ArrayEqual(stringToGBA));

        }
    }
}
