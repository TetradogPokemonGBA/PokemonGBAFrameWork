using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class BorderTileData
    {
        MapTile[,] MapTiles { get; set; }
        
        public OffsetRom Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] DataToLoad { get; set; }
        public int Size => Width * Height * Word.LENGTH;


        public void EndLoad()
        {
            int index;
            int raw;

            if (!Equals(DataToLoad, default))
            {
                for (int y = 0; y < Height; y++)
                {
                     for (int x = 0; x < Width; x++)
                        {
                        index = (y * Width) + x;
                        raw = new Word(DataToLoad, index * Word.LENGTH);
                        MapTiles[x, y] = new MapTile(raw & 0x3FF, (raw & 0xFC00) >> 10);
                    }
                }
                DataToLoad = default;
            }
        }
        public MapTile Get(int x,int y)
        {
            EndLoad();
            return MapTiles[x, y];
        }

        public MapTile[,] Get(int x, int y, int width, int height)
        {

            MapTile[,] mapTilesClone = new MapTile[width, height];

            EndLoad();

            for (int i = x; i < Width; i++)
            {
                for (int j = y; j < Height; j++)
                {

                    mapTilesClone[i - x, j - y] = MapTiles[i, j];
                }
            }
            return mapTilesClone;
        }



        public Bitmap Render(RomGba rom,Map map,Bitmap tileSet)
        {
            throw new NotImplementedException();
        }

        public static BorderTileData Get(RomGba rom, int offset, MapData mData)
        {
            BorderTileData borderTileData = new BorderTileData();
            borderTileData.Offset = new OffsetRom(offset);
            borderTileData.Width = mData.BorderWidth;
            borderTileData.Height = mData.BorderHeight;
            borderTileData.DataToLoad = rom.Data.SubArray(offset, borderTileData.Size);

            return borderTileData;
        }

    }
}
