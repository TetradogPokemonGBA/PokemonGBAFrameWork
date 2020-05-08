using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class SpriteSign
	{
		public const int LENGTH = 12;



		public SpriteSign(RomGba rom, int offset)
		{

			BX = rom.Data[offset++];
			B2 = rom.Data[offset++];
			BY = rom.Data[offset++];
			B4 = rom.Data[offset++];
			B5 = rom.Data[offset++];
			B6 = rom.Data[offset++];
			B7 = rom.Data[offset++];
			B8 = rom.Data[offset++];
			PScript = new OffsetRom(rom, offset);
		}

		public SpriteSign(byte x, byte y)
		{
			BX = x;
			B2 = 0;
			BY = y;
			B4 = 0;
			B5 = 0;
			B6 = 0;
			B7 = 0;
			B8 = 0;
			PScript = new OffsetRom(0);
		}

		public byte BX { get; set; }
		public byte B2 { get; set; }
		public byte BY { get; set; }
		public byte B4 { get; set; }
		public byte B5 { get; set; }
		public byte B6 { get; set; }
		public byte B7 { get; set; }
		public byte B8 { get; set; }
		public OffsetRom PScript { get; set; }

		public byte[] GetBytes()
		{
			byte[] data = new byte[] { BX, B2, BY, B4, B5, B6, B7, B8 };
			return data.AddArray(new OffsetRom(PScript + (PScript == 0 ? 0 : 0x08000000)).BytesPointer);
		}
	}

}
