using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class HeaderSprites
	{

		public byte NumNPC { get; set; }
		public byte NumExits { get; set; }
		public byte NumTraps { get; set; }
		public byte NumSigns { get; set; }
		public OffsetRom OffsetNPC { get; set; }
		public OffsetRom OffsetExits { get; set; }
		public OffsetRom OffsetTraps { get; set; }
		public OffsetRom OffsetSigns { get; set; }

		public static HeaderSprites Get(RomGba rom, int offset)
		{
			HeaderSprites headerSprites = new HeaderSprites();
			int offsetData = offset & 0x1FFFFFF;
			headerSprites.NumNPC = rom.Data[offsetData++];
			headerSprites.NumExits = rom.Data[offsetData++];
			headerSprites.NumTraps = rom.Data[offsetData++];
			headerSprites.NumSigns = rom.Data[offsetData++];
			headerSprites.OffsetNPC = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			headerSprites.OffsetExits = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			headerSprites.OffsetTraps = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			headerSprites.OffsetSigns = new OffsetRom(rom, offsetData);
			return headerSprites;
		}
	}
}
