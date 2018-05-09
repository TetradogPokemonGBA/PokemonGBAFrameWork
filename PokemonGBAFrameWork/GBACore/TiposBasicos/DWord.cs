using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class DWord : BaseWord
    {
        public const int LENGTH = 4;

        public DWord(uint dword) : base(Serializar.GetBytes(dword)) { }
        public DWord(byte[] data) : base(data)
        {
        }

        public DWord(RomGba rom, int offsetDWord) : base(rom, offsetDWord, LENGTH)
        {
        }

        public DWord(BloqueBytes rom, int offsetDWord) : base(rom, offsetDWord, LENGTH)
        {
        }

        public DWord(byte[] rom, int offsetDWord) : base(rom, offsetDWord, LENGTH)
        {
        }

        public unsafe DWord(byte* ptrRom, int offsetDWord) : base(ptrRom, offsetDWord, LENGTH)
        {
        }

        public static implicit operator uint(DWord word)
        {
            return Serializar.ToUInt(word.Data);
        }

        public static implicit operator DWord(uint word)
        {
            return new DWord(word);
        }
        public static explicit operator DWord(Hex word)
        {
            return (uint)word;
        }


    
}
}
