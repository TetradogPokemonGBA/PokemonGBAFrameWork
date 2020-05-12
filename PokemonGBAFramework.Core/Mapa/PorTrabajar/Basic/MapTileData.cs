using Gabriel.Cat.S.Drawing;
using Gabriel.Cat.S.Extension;
using PokemonGBAFramework.Core.Mapa.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class MapTileData
	{

		private MapTile[,] mapTiles;

		public int Columnas =>mapTiles.GetLength(DimensionMatriz.Columna);
			
		public int Filas  =>mapTiles.GetLength(DimensionMatriz.Fila);
		public int Size =>Columnas * Filas * MapTile.LENGTH;
		public Bitmap GetBitmap(Tileset tileset,GranPaleta paleta=default)
		{
			Collage collage = new Collage();
			
			if (Equals(paleta, default))
				paleta = tileset.Paletas.FirstOrDefault();
			if (Equals(paleta, default))
				throw new ArgumentNullException(nameof(paleta));

			for (int y = 0, xF = Columnas, yF = Filas; y < yF; y++)
				for (int x = 0; x < xF; x++)
					collage.Add(tileset.Get(Get(x,y).ID).Get(paleta), x*Tile.LADO, y * Tile.LADO);

			return collage.CrearCollage();

		}
		public MapTile Get(int x, int y)
		{
			MapTile tile;
			if (x < 0 && y < 0)
				tile = mapTiles[0, 0];
			else if (x < 0)
				tile = mapTiles[0, y];
			else if (y < 0)
				tile = mapTiles[x, 0];
			else if (x >=Columnas && y >= Filas)
				tile = mapTiles[Columnas-1, Filas-1];
			else if (x >= Columnas)
				tile = mapTiles[Columnas-1, y];
			else if (y >= Filas)
				tile = mapTiles[x, Filas-1];
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
		public static MapTileData Get(RomGba rom, MapData mData,int offsetMapTiles)
		{
			int index;
			int raw;
			MapTile m;

			MapTileData mapTileData = new MapTileData();
			mapTileData.mapTiles = new MapTile[(uint)mData.Width, (uint)mData.Height];

			if (!Equals(offsetMapTiles, default))
			{
				for (int x = 0; x < mapTileData.Columnas; x++)
				{
					for (int y = 0; y < mapTileData.Filas; y++)
					{

						index = (int)((y * mData.Width) + x);
						raw = new Word(rom, offsetMapTiles + index * 2);
						m = new MapTile((raw & 0x3FF), (raw & 0xFC00) >> 10);
						mapTileData.mapTiles[x, y] = m;

					}
				}
			}
			return mapTileData;
		}
	}

}
