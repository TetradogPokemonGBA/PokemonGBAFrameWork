using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class BorderMap
    {
        public BorderMap(RomGba rom, Map map)
        {
            Map = map;
            MapTileData =  BorderTileData.Get(rom, MapData.OffsetBorderTile, MapData);
        }

        public BorderTileData MapTileData { get; set; }
        public MapData MapData => Map.MapData;

        public Map Map { get; set; }
        public bool IsEdited { get; set; } 
    }

}
