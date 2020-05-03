using System;

namespace PokemonGBAFramework.Core
{
    public class NombreAtaque : IComparable<NombreAtaque>
    {
        public BloqueString Texto { get; set; }

        public int CompareTo(NombreAtaque other)
        {
            int compareTo;
            if (other != default)
            {
                compareTo = Texto.CompareTo(other.Texto);
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        public override string ToString()
        {
            return Texto.ToString();
        }

        public static NombreAtaque Get(RomGba rom, int posicionAtaque)
        {
            throw new NotImplementedException();
        }
    }
}