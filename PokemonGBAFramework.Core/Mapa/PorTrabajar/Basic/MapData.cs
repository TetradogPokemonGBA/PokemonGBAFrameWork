using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class MapData
	{
		public MapData(){	}
		public MapHeader MapHeader { get; set; }
		public Word Width { get; set; }
		public Word Height { get; set; }
		public OffsetRom OffsetBorderTile { get; set; }
		public OffsetRom OffsetMapTiles { get; set; }
		public OffsetRom OffsetGlobalTileset { get; set; }
		public OffsetRom OffsetLocalTileset { get; set; }
		public Word BorderWidth { get; set; }
		public Word BorderHeight { get; set; }
		public Word SecondarySize=> new Word((ushort)(BorderWidth + 0xA0));


		public static MapData Get(RomGba rom, MapHeader mapHeader)
		{
		
			MapData mapData = new MapData();
			int offsetMap = mapHeader.OffsetMap;
			mapData.MapHeader = mapHeader;
			mapData.Width =new Word(rom,new OffsetRom(rom, offsetMap ));
			offsetMap += OffsetRom.LENGTH;
			mapData.Height = new Word(rom, new OffsetRom(rom,offsetMap));
			offsetMap += OffsetRom.LENGTH;
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

			if (mapData.OffsetBorderTile.IsEmpty)
				mapData.OffsetBorderTile = default;
			else mapData.OffsetBorderTile.Fix();

			if (mapData.OffsetMapTiles.IsEmpty)
				mapData.OffsetMapTiles = default;
			else mapData.OffsetMapTiles.Fix();

			if (mapData.OffsetGlobalTileset.IsEmpty)
				mapData.OffsetGlobalTileset = default;
			else mapData.OffsetGlobalTileset.Fix();

			if (mapData.OffsetLocalTileset.IsEmpty)
				mapData.OffsetLocalTileset = default;
			else mapData.OffsetLocalTileset.Fix();


			return mapData;
		}

}

}
