using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class MapTileData
	{
	private int originalPointer;
	private int originalSize;
	private MapData mData;
	private MapTile[,] mapTiles;
	public MapTileData(RomGba rom, MapData mData)
	{
			int index;
			int raw;
			MapTile m;
		this.mData = mData;
		mapTiles = new MapTile[(int)mData.MapWidth,(int)mData.MapHeight];
		for (int x = 0; x < mData.MapWidth; x++)
		{
			for (int y = 0; y < mData.MapHeight; y++)
			{

				 index = (int)((y * mData.MapWidth) + x);
				 raw = new Word(rom,mData.MapTilesPtr + index * 2);
				 m = new MapTile((raw & 0x3FF), (raw & 0xFC00) >> 10);
				mapTiles[x,y] = m;

			}
		}
		this.originalPointer = mData.MapTilesPtr;
		this.originalSize = getSize();
	}

		public MapTile getTile(int x, int y)
		{
			MapTile tile;
			if (x < 0 && y < 0)
				tile = mapTiles[0, 0];
			else if (x < 0)
				tile = mapTiles[0, y];
			else if (y < 0)
				tile = mapTiles[x, 0];

			if (x > mData.MapWidth && y > mData.MapHeight)
				tile = mapTiles[(int)mData.MapWidth, (int)mData.MapHeight];
			else if (x > mData.MapWidth)
				tile = mapTiles[(int)mData.MapWidth, y];
			else if (y > mData.MapHeight)
				tile = mapTiles[x, (int)mData.MapHeight];

			else tile = mapTiles[x, y];
			return tile;
		}

	public MapTile[,] getTiles(int x, int y, int width, int height)
	{
		MapTile[,] m = new MapTile[width,height];
		for (int i = x; i < x + width; i++)
		{
			for (int j = y; j < y + width; j++)
			{
				m[i - x,j - y] = getTile(i, j);
			}
		}
		return m;
	}

	public int getSize()
	{
		return (int)((mData.MapWidth * mData.MapHeight) * 2);
	}

	//public void save()
	//{
	//	for (int x = 0; x < mData.mapWidth; x++)
	//	{
	//		for (int y = 0; y < mData.mapHeight; y++)
	//		{

	//			int index = (int)((y * mData.mapWidth) + x);
	//			rom.writeWord(BitConverter.shortenPointer(mData.mapTilesPtr) + index * 2, mapTiles[x][y].getID() + ((mapTiles[x][y].getMeta() & 0x3F) << 10));
	//		}
	//	}
	//}

	public void resize(RomGba rom,long xSize, long ySize)
	{
		MapTile[,] newMapTiles = new MapTile[(int)xSize,(int)ySize];
		mData.MapWidth = xSize;
		mData.MapHeight = ySize;
		//rom.floodBytes(originalPointer, rom.freeSpaceByte, originalSize);

		//TODO make this a setting, ie always repoint vs keep pointers
		if (originalSize < getSize())
		{
			mData.MapTilesPtr = rom.Data.SearchEmptyBytes(getSize());
		}

		for (int x = 0; x < xSize; x++)
			for (int y = 0; y < ySize; y++)
			{
				try
				{
					newMapTiles[x,y] = mapTiles[x,y];
				}
				catch (Exception e)
				{
					newMapTiles[x,y] = new MapTile(0, 0);
				}
			}

		mapTiles = newMapTiles;
	}
}

}
