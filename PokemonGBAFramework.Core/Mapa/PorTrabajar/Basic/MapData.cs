using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class MapData
	{


		public MapData(RomGba rom, MapHeader mHeader,int localTSize,int engine=0)
		{
			MapHeader = mHeader;
			load(rom,localTSize,engine);
		}
		public MapHeader MapHeader { get; set; }
		public long MapWidth { get; set; }
		public long MapHeight { get; set; }
		public int BorderTilePtr { get; set; }
		public int MapTilesPtr { get; set; }
		public int GlobalTileSetPtr { get; set; }
		public int LocalTileSetPtr { get; set; }
		public int BorderWidth { get; set; }
		public int BorderHeight { get; set; }
		public int SecondarySize { get; set; }

		public int load(RomGba rom,int localTSSize, int engine=0)
		{
			int secondSize;
			MapWidth = new OffsetRom(rom, ((int)MapHeader.OffsetMap));
			MapHeight = new OffsetRom(rom, ((int)MapHeader.OffsetMap) + 0x4);
			BorderTilePtr = new OffsetRom(rom, ((int)MapHeader.OffsetMap) + 0x8);
			MapTilesPtr = new OffsetRom(rom, ((int)MapHeader.OffsetMap) + 0xC);
			GlobalTileSetPtr = new OffsetRom(rom, ((int)MapHeader.OffsetMap) + 0x10);
			LocalTileSetPtr = new OffsetRom(rom,((int)MapHeader.OffsetMap) + 0x14);
			BorderWidth = new Word(rom,((int)MapHeader.OffsetMap) + 0x18);
			BorderHeight = new Word(rom, ((int)MapHeader.OffsetMap) + 0x1A);
			SecondarySize = BorderWidth + 0xA0;
			//System.out.println(borderWidth + " " + borderHeight);
			if (engine == 0) //If this is a RSE game...
			{
				BorderWidth = 2;
				BorderHeight = 2;
				secondSize = SecondarySize;
			}
			else
			{
				SecondarySize = localTSSize;
				secondSize = localTSSize;
			}
			return secondSize;
		}


	//public void save()
	//{
	//	rom.Seek(((int)mapHeader.pMap));
	//	rom.writePointer(mapWidth);
	//	rom.writePointer(mapHeight);
	//	rom.writePointer(borderTilePtr);
	//	rom.writePointer(mapTilesPtr);
	//	rom.writePointer(globalTileSetPtr);
	//	rom.writePointer(localTileSetPtr);
	//	//rom.writeBytes(((int)mapHeader.pMap), new byte[]{(byte)(borderWidth), (byte)(borderHeight)}); //Isn't quite working yet :/
	//}
}

}
