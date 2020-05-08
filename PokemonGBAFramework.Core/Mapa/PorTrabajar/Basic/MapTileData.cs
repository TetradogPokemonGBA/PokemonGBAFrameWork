using Gabriel.Cat.S.Extension;
using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class MapTileData
	{
		private MapData mData;
		private MapTile[,] mapTiles;
		public MapTileData(RomGba rom, MapData mData)
		{
			int index;
			int raw;
			MapTile m;
			this.mData = mData;
			mapTiles = new MapTile[(uint)mData.MapWidth, (uint)mData.MapHeight];
			for (int x = 0; x < mData.MapWidth; x++)
			{
				for (int y = 0; y < mData.MapHeight; y++)
				{

					index = (int)((y * mData.MapWidth) + x);
					raw = new Word(rom, mData.MapTilesPtr + index * 2);
					m = new MapTile((raw & 0x3FF), (raw & 0xFC00) >> 10);
					mapTiles[x, y] = m;

				}
			}
		}
		public int Size => (int)((mData.MapWidth * mData.MapHeight) * 2);
		public MapTile Get(int x, int y)
		{
			MapTile tile;
			if (x < 0 && y < 0)
				tile = mapTiles[0, 0];
			else if (x < 0)
				tile = mapTiles[0, y];
			else if (y < 0)
				tile = mapTiles[x, 0];
			else if (x > mData.MapWidth && y > mData.MapHeight)
				tile = mapTiles[(uint)mData.MapWidth, (uint)mData.MapHeight];
			else if (x > mData.MapWidth)
				tile = mapTiles[(uint)mData.MapWidth, y];
			else if (y > mData.MapHeight)
				tile = mapTiles[x, (uint)mData.MapHeight];
			else tile = mapTiles[x, y];

			return tile;
		}

		public MapTile[,] Get(int x, int y, int width, int height)
		{
			MapTile[,] mapTiles = new MapTile[width, height];
			for (int i = x,xF= x + width, yF= y + height; i < xF; i++)
			{
				for (int j = y; j < yF; j++)
				{
					mapTiles[i - x, j - y] = Get(i, j);
				}
			}
			return mapTiles;
		}




		public void Resize(int xSize, int ySize)
		{
			MapTile[,] newMapTiles = new MapTile[xSize, ySize];
			mData.MapWidth = (uint)xSize;
			mData.MapHeight = (uint)ySize;

			for (int x = 0, xOld = mapTiles.GetLength(DimensionMatriz.X), yOld = mapTiles.GetLength(DimensionMatriz.Y); x < xSize; x++)
				for (int y = 0; y < ySize; y++)
				{
					if (x < xOld && y < yOld)
						newMapTiles[x, y] = mapTiles[x, y];
					else
						newMapTiles[x, y] = new MapTile();

				}

			mapTiles = newMapTiles;
		}
	}

}
