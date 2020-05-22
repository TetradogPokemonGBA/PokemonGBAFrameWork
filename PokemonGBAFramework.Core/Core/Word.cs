using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Word : BaseWord
    {

        public const int LENGTH = 2;


        public override string ToString()
        {
            return (ushort)this+"";
        }
        #region Constructores
        public Word() : this(0) { }
        public Word(ushort word) : base(Serializar.GetBytes(word)) { }
        public Word(byte[] data) : base(data)
        {
        }
        public Word(RomGba data) : base(data.Data.Bytes)
        {
        }
        public Word(RomGba rom, int offset) : base(rom, offset, LENGTH)
        {
        }

        public Word(BloqueBytes rom, int offset) : base(rom, offset, LENGTH)
        {
        }

        public Word(byte[] rom, int offsetWord) : base(rom, offsetWord, LENGTH)
        {
        }

        public unsafe Word(byte* ptrRom, int offsetWord) : base(ptrRom, offsetWord, LENGTH)
        {
        }
        #endregion
        #region Conversiones
        public static implicit operator ushort(Word word)
        {

            return Serializar.ToUShort(word.Data.InvertirClone());
        }

        public static implicit operator Word(ushort word)
        {
            return new Word(word);
        }
        public static explicit operator Word(Hex word)
        {
            return (ushort)word;
        }
        #endregion

    }
}