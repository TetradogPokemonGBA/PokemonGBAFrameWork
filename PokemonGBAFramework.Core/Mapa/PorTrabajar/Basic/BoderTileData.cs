using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class BorderTileData
	{
		private MapData mData;
		private MapTile[,] mapTiles;


		public int Size=> mData.BorderWidth * mData.BorderHeight * Word.LENGTH;

		public MapTile Get(RomGba rom, OffsetRom offset,int x, int y)
		{
			MapTile tile;
			int raw;
			int index;

			if (mapTiles[x,y] != default)
				tile= mapTiles[x,y];
			else
			{
				index = (y * mData.BorderWidth) + x;
			    raw =new Word(rom, offset + index * Word.LENGTH);
				tile= new MapTile(raw & 0x3FF, (raw & 0xFC00) >> 10);
				mapTiles[x,y] = tile;
				
			}
			return tile;
		}


		public MapTile[,] Get(RomGba rom,OffsetRom offset,int x, int y, int width, int height)
		{
			MapTile[,] mapTiles = new MapTile[width,height];
			for (int i = x; i < x + width; i++)
			{
				for (int j = y; j < y + width; j++)
				{
					mapTiles[i - x,j - y] = Get(rom,offset,i, j);
				}
			}
			return mapTiles;
		}

		public byte[] GetBytes()
		{

			byte[] data = new byte[Size];
			int dataLoc = 0;
			for (int x = 0; x < mData.BorderWidth; x++)
			{
				for (int y = 0; y < mData.BorderHeight; y++)
				{
					Word.SetData(data,dataLoc,(Word)(ushort)(mapTiles[y,x].ID + ((mapTiles[y,x].Meta & 0x3F) << 10)));
					dataLoc += Word.LENGTH;
				}
			}
			return data;
		}
		public static BorderTileData Get(RomGba rom, OffsetRom offset, MapData mData)
		{
			BorderTileData borderTileData = new BorderTileData();
			borderTileData.mData = mData;
			borderTileData.mapTiles = new MapTile[mData.BorderWidth, mData.BorderHeight];
			for (int x = 0; x < mData.BorderWidth; x++)
			{
				for (int y = 0; y < mData.BorderHeight; y++)
				{
					borderTileData.mapTiles[x, y] = borderTileData.Get(rom, offset, x, y);
				}
			}
			return borderTileData;
		}
		
	}
}
