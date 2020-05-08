using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class BorderMap
    {
        private Map map;
        private MapData mapData;
        private BorderTileData mapTileData;
        public bool isEdited = false;
        public BorderMap(RomGba rom, Map m)
        {
            map = m;
            mapData = map.getMapData();
            mapTileData = new BorderTileData(rom, mapData.BorderTilePtr, mapData);
        }

        public MapData getMapData()
        {
            return mapData;
        }

        public BorderTileData getMapTileData()
        {
            return mapTileData;
        }

        //public void save()
        //{
        //    mapTileData.Save();
        //}
    }

}
