using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class SpriteSign:SpriteBase
	{
		public const int LENGTH = 12;
		public SpriteSign():this(0,0) 
		{ }
		public SpriteSign(byte x, byte y):base(x,y)
		{
			B2 = 0;
			B4 = 0;
			B5 = 0;
			B6 = 0;
			B7 = 0;
			B8 = 0;
			OffsetScript = default;
		}

		public byte B2 { get; set; }
		public byte B4 { get; set; }
		public byte B5 { get; set; }
		public byte B6 { get; set; }
		public byte B7 { get; set; }
		public byte B8 { get; set; }
		public OffsetRom OffsetScript { get; set; }

		public override byte[] GetBytes()
		{
			byte[] data = new byte[] { X, B2, Y, B4, B5, B6, B7, B8 };
			return data.AddArray(Equals(OffsetScript,default)?new byte[OffsetRom.LENGTH]:OffsetScript.BytesPointer);
		}
		public static SpriteSign Get(ScriptManager scriptManager,RomGba rom, int offset)
		{
			SpriteSign spriteSign = new SpriteSign();
			spriteSign.X = rom.Data[offset++];
			spriteSign.B2 = rom.Data[offset++];
			spriteSign.Y = rom.Data[offset++];
			spriteSign.B4 = rom.Data[offset++];
			spriteSign.B5 = rom.Data[offset++];
			spriteSign.B6 = rom.Data[offset++];
			spriteSign.B7 = rom.Data[offset++];
			spriteSign.B8 = rom.Data[offset++];
			spriteSign.OffsetScript = new OffsetRom(rom, offset);
			return spriteSign;
		}
	}

}
