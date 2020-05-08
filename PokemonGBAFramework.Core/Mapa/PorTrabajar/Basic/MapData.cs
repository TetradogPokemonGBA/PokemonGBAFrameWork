using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class MapData
	{
		public MapData(){	}
		public MapHeader MapHeader { get; set; }
		public DWord MapWidth { get; set; }
		public DWord MapHeight { get; set; }
		public OffsetRom BorderTilePtr { get; set; }
		public OffsetRom MapTilesPtr { get; set; }
		public OffsetRom GlobalTileSetPtr { get; set; }
		public OffsetRom LocalTileSetPtr { get; set; }
		public Word BorderWidth { get; set; }
		public Word BorderHeight { get; set; }
		public Word SecondarySize=> new Word((ushort)(BorderWidth + 0xA0));


		public static MapData Get(RomGba rom, MapHeader mapHeader)
		{
			return Get(rom, mapHeader.OffsetMap);
		}
		public static MapData Get(RomGba rom,OffsetRom offsetMapHeader)
		{
			MapData mapData = new MapData();
			int offsetMap = offsetMapHeader;
			mapData.MapWidth = new DWord(rom, offsetMap );
			offsetMap += DWord.LENGTH;
			mapData.MapHeight = new DWord(rom,offsetMap);
			offsetMap += DWord.LENGTH;
			mapData.BorderTilePtr = new OffsetRom(rom, offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.MapTilesPtr = new OffsetRom(rom, offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.GlobalTileSetPtr = new OffsetRom(rom, offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.LocalTileSetPtr = new OffsetRom(rom,offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.BorderWidth = new Word(rom,offsetMap);
			offsetMap += Word.LENGTH;
			mapData.BorderHeight = new Word(rom, offsetMap);
			return mapData;
		}

}

}
