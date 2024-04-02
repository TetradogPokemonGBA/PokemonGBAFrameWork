using PokemonGBAFramework.Core.Mapa.Basic.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
//https://github.com/shinyquagsire23/GBAUtils/blob/master/src/org/zzl/minegaming/GBAUtils/GBARom.java por mirar...
namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class MapData
    {
        const int ToPx = 16;
        public MapData() { }


        public OffsetRom Offset { get; set;}
        public int WidthPx => Width * ToPx;
        public DWord Width { get; set; }
        public int HeightPx => Height * ToPx;
        public DWord Height { get; set; }
        public BorderTileData BorderTileData { get; set; }
        public MapTileData MapTiles { get; set; }
        public Tileset GlobalTileset { get; set; }
        public Tileset LocalTileset { get; set; }
        public int BorderWidthPx => BorderWidth * ToPx;
        public byte BorderWidth { get; set; }
        public int BorderHeightPx=>BorderHeight * ToPx;
        public byte BorderHeight { get; set; }
        public Word SecondarySize { get; set; }

        public Bitmap GetMapEmpty()
        {
            return new Bitmap(WidthPx,HeightPx);
        }
        public Bitmap GetBorderEmpty()
        {
            return new Bitmap(BorderWidthPx,BorderHeightPx);
        }
        public Bitmap GetBitmap(Tileset tileset = default)
        {
            if (Equals(tileset, default))
                tileset = GlobalTileset;
            return MapTiles.GetBitmap(tileset);
        }
        public static MapData Get(RomGba rom, int offsetMapData,TilesetCache tilesetCache, OffsetRom offsetTilesets =default)
        {
            OffsetRom offsetLocalTileset;
            OffsetRom offsetBorderTile;
            OffsetRom offsetGlobalTileset;
            OffsetRom offsetMapTiles;

            MapData mapData = new MapData();
            int offsetMap = offsetMapData;
            mapData.Offset =new OffsetRom(offsetMapData);

            mapData.Width = new DWord(rom, offsetMap);
            offsetMap += DWord.LENGTH;
            mapData.Height = new DWord(rom, offsetMap);
            offsetMap += DWord.LENGTH;
            
            offsetBorderTile = new OffsetRom(rom, offsetMap);//border
            offsetMap += OffsetRom.LENGTH;
            offsetMapTiles = new OffsetRom(rom, offsetMap);//mapData
           
            offsetMap += OffsetRom.LENGTH;
            offsetGlobalTileset = new OffsetRom(rom, offsetMap);//majorTileset
            offsetMap += OffsetRom.LENGTH;
            offsetLocalTileset = new OffsetRom(rom, offsetMap);//minorTileset
            offsetMap += OffsetRom.LENGTH;
            if (rom.Edicion.EsKanto)
            {
                mapData.BorderWidth = rom.Data[offsetMap++];
             
                mapData.BorderHeight = rom.Data[offsetMap++];

            }
            else
            {
                mapData.BorderWidth = 0x2;
                mapData.BorderHeight = 0x2;
            }
       

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
                mapData.GlobalTileset = tilesetCache.Get(rom, offsetGlobalTileset, offsetTilesets);
            }
            else
            {
                mapData.GlobalTileset = default;
            }

            if (!offsetLocalTileset.IsEmpty)
            {
                mapData.LocalTileset = tilesetCache.Get(rom, offsetLocalTileset, offsetTilesets);
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
