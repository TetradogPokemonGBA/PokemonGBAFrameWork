using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class TilesetCache
	{
		private  SortedList<int, Tileset> cache = new SortedList<int, Tileset>();

		private TilesetCache() { }

		public  bool Contains(int offset)
		{
			return cache.ContainsKey(offset);
		}

		public  Tileset Get(RomGba rom,OffsetRom offset)
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

		public  void Clear()
		{
			cache.Clear();
		}


	}

}
