using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class Trigger
	{
		public const int LENGTH = 16;
		public Trigger(RomGba rom, int offset)
		{

			BX = rom.Data[offset++];
			B2 = rom.Data[offset++];
			BY = rom.Data[offset++];
			B4 = rom.Data[offset++];
			H3 = new Word(rom, offset);
			offset += Word.LENGTH;
			HFlagCheck = new Word(rom, offset);
			offset += Word.LENGTH;
			HFlagValue = new Word(rom, offset);
			offset += Word.LENGTH;
			H6 = new Word(rom, offset);
			offset += Word.LENGTH;
			PScript = new OffsetRom(rom, offset);
		}


		public Trigger(byte x, byte y)
		{
			BX = x;
			B2 = 0;
			BY = y;
			B4 = 0;
			H3 = 0;
			HFlagCheck = 0;
			HFlagValue = 0;
			H6 = 0;
			PScript = new OffsetRom(0);
		}



		public byte BX { get; set; }
		public byte B2 { get; set; }
		public byte BY { get; set; }
		public byte B4 { get; set; }
		public Word H3 { get; set; }
		public Word HFlagCheck { get; set; }
		public Word HFlagValue { get; set; }
		public Word H6 { get; set; }
		public OffsetRom PScript { get; set; }

		public byte[] GetBytes()
		{
			byte[] data = { BX, B2, BY, B4, };
			return data.AddArray(H3.Data, HFlagCheck.Data, H6.Data, new OffsetRom(PScript + (PScript == 0 ? 0 : 0x08000000)).BytesPointer);

		}
	}

}
