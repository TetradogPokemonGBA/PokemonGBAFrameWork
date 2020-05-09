using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class MapData
	{
		public MapData(){	}
		public MapHeader MapHeader { get; set; }
		public DWord Width { get; set; }
		public DWord Height { get; set; }
		public OffsetRom OffsetBorderTile { get; set; }
		public OffsetRom OffsetMapTiles { get; set; }
		public OffsetRom OffsetGlobalTileset { get; set; }
		public OffsetRom OffsetLocalTileset { get; set; }
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
			mapData.Width = new DWord(rom, offsetMap );
			offsetMap += DWord.LENGTH;
			mapData.Height = new DWord(rom,offsetMap);
			offsetMap += DWord.LENGTH;
			mapData.OffsetBorderTile = new OffsetRom(rom, offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.OffsetMapTiles = new OffsetRom(rom, offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.OffsetGlobalTileset = new OffsetRom(rom, offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.OffsetLocalTileset = new OffsetRom(rom,offsetMap);
			offsetMap += OffsetRom.LENGTH;
			mapData.BorderWidth = new Word(rom,offsetMap);
			offsetMap += Word.LENGTH;
			mapData.BorderHeight = new Word(rom, offsetMap);
			return mapData;
		}

}

}
