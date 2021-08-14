﻿using PokemonGBAFramework.Core.Mapa.Basic.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
//https://github.com/shinyquagsire23/GBAUtils/blob/master/src/org/zzl/minegaming/GBAUtils/GBARom.java por mirar...
namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class MapData
    {
        public MapData() { }
        public DWord Width { get; set; }
        public DWord Height { get; set; }
        public BorderTileData BorderTileData { get; set; }
        public MapTileData MapTiles { get; set; }
        public Tileset GlobalTileset { get; set; }
        public Tileset LocalTileset { get; set; }
        public Word BorderWidth { get; set; }
        public Word BorderHeight { get; set; }
        public Word SecondarySize { get; set; }
        public Bitmap GetBitmap(Tileset tileset = default)
        {
            if (Equals(tileset, default))
                tileset = GlobalTileset;
            return MapTiles.GetBitmap(tileset);
        }
        public static MapData Get(RomGba rom, int offsetMapData,TilesetCache tilesetCache)
        {
            OffsetRom offsetLocalTileset;
            OffsetRom offsetBorderTile;
            OffsetRom offsetGlobalTileset;
            OffsetRom offsetMapTiles;

            MapData mapData = new MapData();
            int offsetMap = offsetMapData;

            mapData.Width = new DWord(rom, offsetMap);
            offsetMap += DWord.LENGTH;
            mapData.Height = new DWord(rom, offsetMap);
            offsetMap += DWord.LENGTH;
            
            offsetBorderTile = new OffsetRom(rom, offsetMap);
            offsetMap += OffsetRom.LENGTH;
            offsetMapTiles = new OffsetRom(rom, offsetMap);
            offsetMap += OffsetRom.LENGTH;
            offsetGlobalTileset = new OffsetRom(rom, offsetMap);
            offsetMap += OffsetRom.LENGTH;
            offsetLocalTileset = new OffsetRom(rom, offsetMap);
            offsetMap += OffsetRom.LENGTH;
           
            mapData.BorderWidth = new Word(rom,offsetMap);
            offsetMap += Word.LENGTH;
            mapData.BorderHeight = new Word(rom,offsetMap);

            if (!offsetBorderTile.IsEmpty)
            {
                mapData.BorderTileData = BorderTileData.Get(rom, offsetBorderTile, mapData);
            }
            else
            {
                mapData.BorderTileData = default;
            }
            if (!offsetMapTiles.IsEmpty)
            {
                mapData.MapTiles = MapTileData.Get(rom, mapData, offsetMapTiles);
            }
            else
            {
                mapData.MapTiles = default;
            }

            if (!offsetGlobalTileset.IsEmpty)
            {
                mapData.GlobalTileset = tilesetCache.Get(rom, offsetGlobalTileset);
            }
            else
            {
                mapData.GlobalTileset = default;
            }

            if (!offsetLocalTileset.IsEmpty)
            {
                mapData.LocalTileset = tilesetCache.Get(rom, offsetLocalTileset);
            }
            else
            {
                mapData.LocalTileset = default;
            }

            if (rom.Edicion.EsHoenn)
            {
                mapData.SecondarySize = new Word((ushort)(mapData.BorderWidth + 0xA0));
                mapData.BorderWidth = 2;
                mapData.BorderHeight = 2;
            }
            else
            {
                mapData.SecondarySize = (ushort)TilesetHeader.GetLocalSize(rom);
            }

            return mapData;
        }


        public static bool Check(RomGba rom,int offsetMapData)
        {
            bool isOK;
            offsetMapData += OffsetRom.LENGTH;
            offsetMapData += OffsetRom.LENGTH;
            isOK =  OffsetRom.Check(rom,offsetMapData);
            if (isOK)
            {
                offsetMapData += OffsetRom.LENGTH;
                isOK = OffsetRom.Check(rom, offsetMapData);
                if (isOK)
                {
                    offsetMapData += OffsetRom.LENGTH;
                    isOK = OffsetRom.Check(rom, offsetMapData);
                }
            }
            return isOK;
        }
    }

}
