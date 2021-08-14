using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class BorderTileData
    {
        MapTile[,] MapTiles { get; set; }

        int Width { get; set; }
        int Height { get; set; }
        byte[] DataToLoad { get; set; }
        public int Size => Width * Height * Word.LENGTH;


        public void EndLoad()
        {
            int index;
            int raw;

            if (!Equals(DataToLoad, default))
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        index = (i * Width) + j;
                        raw = new Word(DataToLoad, index * Word.LENGTH);
                        MapTiles[i, j] = new MapTile(raw & 0x3FF, (raw & 0xFC00) >> 10);
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

        public byte[] GetBytes()
        {

            byte[] data = new byte[Size];
            int dataLoc = 0;
            EndLoad();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Word.SetData(data, dataLoc, (Word)(ushort)(MapTiles[y, x].ID + ((MapTiles[y, x].Meta & 0x3F) << 10)));
                    dataLoc += Word.LENGTH;
                }
            }
            return data;
        }
        public static BorderTileData Get(RomGba rom, int offset, MapData mData)
        {
            BorderTileData borderTileData = new BorderTileData();
            borderTileData.Width = mData.BorderWidth;
            borderTileData.Height = mData.BorderHeight;
            borderTileData.DataToLoad = rom.Data.SubArray(offset, borderTileData.Size);

            return borderTileData;
        }

    }
}
