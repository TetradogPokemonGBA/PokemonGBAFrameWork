using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class HeaderSprites
	{
		public HeaderSprites(RomGba rom, int offset)
		{
			int offsetData = offset & 0x1FFFFFF;
			BNumNPC = rom.Data[offsetData++];
			BNumExits = rom.Data[offsetData++];
			BNumTraps = rom.Data[offsetData++];
			BNumSigns = rom.Data[offsetData++];
			PNPC =new OffsetRom(rom,offsetData);
			offsetData += OffsetRom.LENGTH;
			PExits = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			PTraps = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			PSigns = new OffsetRom(rom, offsetData);
		}
		public byte BNumNPC { get; set; }
		public byte BNumExits { get; set; }
		public byte BNumTraps { get; set; }
		public byte BNumSigns { get; set; }
		public OffsetRom PNPC { get; set; }
		public OffsetRom PExits { get; set; }
		public OffsetRom PTraps { get; set; }
		public OffsetRom PSigns { get; set; }

		//public void save()
		//{
		//	rom.Seek(pData & 0x1FFFFFF);
		//	rom.writeByte(bNumNPC);
		//	rom.writeByte(bNumExits);
		//	rom.writeByte(bNumTraps);
		//	rom.writeByte(bNumSigns);

		//	rom.writePointer((int)pNPC);
		//	rom.writePointer((int)pExits);
		//	rom.writePointer((int)pTraps);
		//	rom.writePointer((int)pSigns);
		//}
	}
}
