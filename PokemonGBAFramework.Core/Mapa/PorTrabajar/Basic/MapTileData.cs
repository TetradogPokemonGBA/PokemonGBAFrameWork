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

		public int Size => (int)((mData.Width * mData.Height) * 2);
		public MapTile Get(int x, int y)
		{
			MapTile tile;
			if (x < 0 && y < 0)
				tile = mapTiles[0, 0];
			else if (x < 0)
				tile = mapTiles[0, y];
			else if (y < 0)
				tile = mapTiles[x, 0];
			else if (x > mData.Width && y > mData.Height)
				tile = mapTiles[(uint)mData.Width, (uint)mData.Height];
			else if (x > mData.Width)
				tile = mapTiles[(uint)mData.Width, y];
			else if (y > mData.Height)
				tile = mapTiles[x, (uint)mData.Height];
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
			mData.Width =(ushort)xSize;
			mData.Height =(ushort)ySize;

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
		public static MapTileData Get(RomGba rom, MapData mData)
		{
			int index;
			int raw;
			MapTile m;

			MapTileData mapTileData = new MapTileData();
			mapTileData.mData = mData;
			mapTileData.mapTiles = new MapTile[(uint)mData.Width, (uint)mData.Height];

			if (!Equals(mData.OffsetMapTiles, default))
			{
				for (int x = 0; x < mData.Width; x++)
				{
					for (int y = 0; y < mData.Height; y++)
					{

						index = (int)((y * mData.Width) + x);
						raw = new Word(rom, mData.OffsetMapTiles + index * 2);
						m = new MapTile((raw & 0x3FF), (raw & 0xFC00) >> 10);
						mapTileData.mapTiles[x, y] = m;

					}
				}
			}
			return mapTileData;
		}
	}

}
