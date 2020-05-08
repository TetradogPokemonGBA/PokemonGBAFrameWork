using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class BorderTileData
	{
		private int originalPointer;
		private int originalSize;
		private int dataLoc;
		private MapData mData;
		private MapTile[,] mapTiles;
		public BorderTileData(RomGba rom, int offset, MapData mData)
		{
			dataLoc = offset;
			this.mData = mData;
			mapTiles = new MapTile[mData.BorderWidth,mData.BorderHeight];
			for (int x = 0; x < mData.BorderWidth; x++)
			{
				for (int y = 0; y < mData.BorderHeight; y++)
				{
					mapTiles[x,y] = getTile(rom,x, y);
				}
			}
			this.originalPointer = mData.BorderTilePtr;
			this.originalSize = getSize();
		}

		public int getSize()
		{
			return (int)((mData.BorderWidth * mData.BorderHeight) * 2);
		}

		public MapTile getTile(RomGba rom,int x, int y)
		{
			if (mapTiles[x,y] != null)
				return mapTiles[x,y];
			else
			{
				int index = (y * mData.BorderWidth) + x;
				int raw =new Word(rom,dataLoc + index * 2);
				MapTile m = new MapTile((raw & 0x3FF), (raw & 0xFC00) >> 10);
				mapTiles[x,y] = m;
				return m;
			}
		}


		public MapTile[,] getTiles(RomGba rom,int x, int y, int width, int height)
		{
			MapTile[,] m = new MapTile[width,height];
			for (int i = x; i < x + width; i++)
			{
				for (int j = y; j < y + width; j++)
				{
					m[i - x,j - y] = getTile(rom,i, j);
				}
			}
			return m;
		}

		public void save(RomGba rom)
		{
	
			for (int x = 0; x < mData.BorderWidth; x++)
			{
				for (int y = 0; y < mData.BorderHeight; y++)
				{

					//int index = (int) ((y*mData.borderWidth) + x);
					Word.SetData(rom,dataLoc,(Word)(ushort)(mapTiles[y,x].ID + ((mapTiles[y,x].Meta & 0x3F) << 10)));
					dataLoc += Word.LENGTH;
				}
			}
		}

		public void resize(long xSize, long ySize)
		{
			/*MapTile[][] newMapTiles = new MapTile[(int)xSize][(int)ySize];
			mData.borderWidth = (int) xSize;
			mData.borderHeight = (int) ySize;
			rom.floodBytes(originalPointer, rom.freeSpaceByte, originalSize);

			//TODO make this a setting, ie always repoint vs keep pointers
			if(originalSize < getSize())
			{
				mData.mapTilesPtr = rom.findFreespace(DataStore.FreespaceStart, getSize());
			}

			for(int x = 0; x < xSize; x++)
				for(int y = 0; y < ySize; y++)
				{
					try
					{
						newMapTiles[x][y] = mapTiles[x][y];
					}
					catch(Exception e)
					{
						newMapTiles[x][y] = new MapTile(0,0);
					}
				}

			mapTiles = newMapTiles;*/
		}
	}
}
