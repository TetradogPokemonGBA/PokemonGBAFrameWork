using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class Trigger:SpriteBase
	{
		public const int LENGTH = 16;

		public Trigger() : this(0, 0) { }
		public Trigger(byte x, byte y):base(x,y)
		{
			B2 = 0;
			B4 = 0;
			H3 = 0;
			FlagCheck = 0;
			FlagValue = 0;
			H6 = 0;
			OffsetScript = default;
		}

		public byte B2 { get; set; }
		public byte B4 { get; set; }
		public Word H3 { get; set; }
		public Word FlagCheck { get; set; }
		public Word FlagValue { get; set; }
		public Word H6 { get; set; }
		public OffsetRom OffsetScript { get; set; }

		public override byte[] GetBytes()
		{
			byte[] data = { X, B2, Y, B4, };
			return data.AddArray(H3.Data, FlagCheck.Data, H6.Data,Equals(OffsetScript,default)?OffsetScript.BytesPointer:new byte[OffsetRom.LENGTH]);

		}
		public static Trigger Get(RomGba rom, int offset)
		{
			Trigger trigger = new Trigger();
			trigger.X = rom.Data[offset++];
			trigger.B2 = rom.Data[offset++];
			trigger.Y = rom.Data[offset++];
			trigger.B4 = rom.Data[offset++];
			trigger.H3 = new Word(rom, offset);
			offset += Word.LENGTH;
			trigger.FlagCheck = new Word(rom, offset);
			offset += Word.LENGTH;
			trigger.FlagValue = new Word(rom, offset);
			offset += Word.LENGTH;
			trigger.H6 = new Word(rom, offset);
			offset += Word.LENGTH;
			trigger.OffsetScript = new OffsetRom(rom, offset);

			if (trigger.OffsetScript.IsEmpty)
				trigger.OffsetScript = default;
			else trigger.OffsetScript.Fix();

			return trigger;
		}
	}

}
