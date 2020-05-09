using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class TilesetCache
	{
		private static SortedList<int, Tileset> cache = new SortedList<int, Tileset>();

		private TilesetCache() { }

		public static bool Contains(int offset)
		{
			return cache.ContainsKey(offset);
		}

		public static Tileset Get(RomGba rom,int offset)
		{
			Tileset tile;
			if (cache.ContainsKey(offset))
			{
				tile = cache[offset];
				if (tile.Modified)
				{
					tile = Tileset.Get(rom, tile.TilesetHeader);
				}
			
			}
			else
			{
				 tile =  Tileset.Get(rom, offset);
				cache.Add(offset, tile);

			}
			return tile;
		}

		public static void Clear()
		{
			cache.Clear();
		}

		public static void switchTileset(RomGba rom,Map loadedMap)
		{
			Get(rom, loadedMap.MapData.GlobalTileSetPtr).RestaurarPaletas();
			Get(rom, loadedMap.MapData.LocalTileSetPtr).RestaurarPaletas();
			for (int j = 1; j < 5; j++)
				for (int i = TilesetHeader.GetPaletaCount(rom) - 1; i < 13; i++)
					Get(rom, loadedMap.MapData.GlobalTileSetPtr).GetPaletas(j - 1)[i] = Get(rom, loadedMap.MapData.LocalTileSetPtr).GetTodasLasPaletas()[j - 1,i];
			for (int j = 0; j < 4; j++)
				Get(rom, loadedMap.MapData.LocalTileSetPtr).SetPaletas(j, Get(rom, loadedMap.MapData.GlobalTileSetPtr).GetPaletas(j));
			Get(rom, loadedMap.MapData.LocalTileSetPtr).Refresh();
			Get(rom, loadedMap.MapData.GlobalTileSetPtr).Refresh();
		}
	}

}
