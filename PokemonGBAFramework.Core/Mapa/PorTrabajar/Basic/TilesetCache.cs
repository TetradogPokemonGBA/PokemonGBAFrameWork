using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class TilesetCache
    {
        private SortedList<int, Tileset> cache;

        public TilesetCache()
        {
            cache = new SortedList<int, Tileset>();
        }

        public bool Contains(int offset)
        {
            return cache.ContainsKey(offset);
        }

        public Tileset Get(RomGba rom, int offset,OffsetRom offsetTilesets = default)
        {
            Tileset tile;
            if (Contains(offset))
            {
                tile = cache[offset];
                if (tile.Modified)
                {
                    tile = Tileset.Get(rom, tile.TilesetHeader);
                    cache[offset] = tile;
                }

            }
            else
            {
                tile = Tileset.Get(rom, offset, offsetTilesets);
                cache.Add(offset, tile);
            }
            return tile;
        }

        public void Clear()
        {
            cache.Clear();
        }


    }

}
