using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class BorderTileData
	{
		private MapTile[,] mapTiles;

		int width;
		int height;
		byte[] data;
		public int Size=> width * height * Word.LENGTH;


		public void EndLoad()
		{
			int index;
			int raw;

			if (!Equals(data, default))
			{
				for (int i = 0; i < this.width; i++)
				{
					for (int j = 0; j < this.height; j++)
					{
						index = (i * width) + j;
						raw = new Word(data, index * Word.LENGTH);
						mapTiles[i,j] = new MapTile(raw & 0x3FF, (raw & 0xFC00) >> 10);
					}
				}
				data = default;
			}
		}

		public MapTile[,] Get(int x, int y, int width, int height)
		{
	
			MapTile[,] mapTilesClone = new MapTile[width,height];

			EndLoad();

			for (int i = x; i < this.width; i++)
			{
				for (int j = y; j < this.height; j++)
				{

					mapTilesClone[i - x, j - y] = this.mapTiles[i,j];
				}
			}
			return mapTilesClone;
		}

		public byte[] GetBytes()
		{

			byte[] data = new byte[Size];
			int dataLoc = 0;
			EndLoad();
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Word.SetData(data,dataLoc,(Word)(ushort)(mapTiles[y,x].ID + ((mapTiles[y,x].Meta & 0x3F) << 10)));
					dataLoc += Word.LENGTH;
				}
			}
			return data;
		}
		public static BorderTileData Get(RomGba rom, int offset, MapData mData)
		{
			BorderTileData borderTileData = new BorderTileData();
			borderTileData.width = mData.BorderWidth;
			borderTileData.height = mData.BorderHeight;
			borderTileData.data = rom.Data.SubArray(offset, borderTileData.Size);

			return borderTileData;
		}
		
	}
}
