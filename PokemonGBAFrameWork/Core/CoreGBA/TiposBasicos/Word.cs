using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
  public  class Word : IComparable, IComparable<Word>
    {
        public const int LENGTH = 2;

        byte[] word;

        public Word(ushort word)
        {
            this.word = Serializar.GetBytes(word);
        }
        public Word(RomData rom, int offsetWord) : this(rom.Rom, offsetWord)
        { }
        public Word(RomGba rom, int offsetWord) : this(rom.Data, offsetWord)
        { }
        public Word(BloqueBytes rom, int offsetWord) : this(rom.Bytes, offsetWord)
        { }
        public Word(byte[] rom, int offsetWord)
        {
            unsafe
            {
                fixed (byte* ptrRom = rom)
                {
                    word = new Word(ptrRom + offsetWord).word;
                }

            }
        }
        public unsafe Word(byte* ptrRom, int offsetWord) : this(ptrRom + offsetWord)
        { }
        public unsafe Word(byte* ptrRomPosicionado)
        {
            word = MetodosUnsafe.ReadBytes(ptrRomPosicionado, LENGTH);
        }
        public byte[] Data
        {
            get { return word; }
        }

        #region IComparable implementation
        public int CompareTo(object obj)
        {
            return CompareTo(obj as Word);
        }
        #endregion
        #region IComparable implementation
        public int CompareTo(Word other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = ((ushort)this).CompareTo((ushort)other);
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;

            return compareTo;
        }
        #endregion
        public override bool Equals(object obj)
        {
            Word other = obj as Word;
            bool isEquals;
            if (other == null)
                isEquals = false;
            else isEquals = this.word.ArrayEqual(other.word);
            return isEquals;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                if (word != null)
                    hashCode += 1000000007 * word.GetHashCode();
            }
            return hashCode;
        }
        public override string ToString()
        {
            return ((ushort)this) + "";
        }
        public static bool operator ==(Word lhs, Word rhs)
        {
            bool iguales;
            if (ReferenceEquals(lhs, rhs))
                iguales = true;
            else if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                iguales = false;
            else iguales = lhs.Equals(rhs);

            return iguales;

        }
        public static void SetWord(RomData rom, int offset, Word word)
        {
            SetWord(rom.Rom, offset, word);
        }
        public static void SetWord(RomGba rom, int offset, Word word)
        {
            SetWord(rom.Data, offset, word);
        }
        public static void SetWord(BloqueBytes datos, int offset, Word word)
        {
            SetWord(datos.Bytes, offset, word);
        }
        public static void SetWord(byte[] datos, int offset, Word word)
        {
            unsafe
            {
                fixed (byte* ptrDatos = datos)
                    SetWord(ptrDatos, offset, word);

            }
        }
        public static unsafe void SetWord(byte* ptrDatos, int offset, Word word)
        {
            SetWord(ptrDatos + offset, word);
        }
        public static unsafe void SetWord(byte* ptrDatosPosicionados, Word word)
        {
            MetodosUnsafe.WriteBytes(ptrDatosPosicionados, word);
        }
        public static bool operator !=(Word lhs, Word rhs)
        {
            return !(lhs == rhs);
        }
        public static implicit operator ushort(Word word)
        {
            return Serializar.ToUShort(word.word);
        }
        public static implicit operator byte[] (Word word)
        {
            return word.word;
        }
        public static implicit operator Word(ushort word)
        {
            return new Word(word);
        }

        public static implicit operator Hex(Word word)
        {
            return (Hex)word.word.ReverseArray();
        }
        public static implicit operator Word(Hex word)
        {
            return new Word((ushort)word);
        }

    }
}