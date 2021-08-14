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

		private MapTile[,] MapTiles { get; set; }

		private byte[] DataToLoad { get; set; }
		public int Columnas =>MapTiles.GetLength(DimensionMatriz.Columna);
			
		public int Filas  =>MapTiles.GetLength(DimensionMatriz.Fila);
		public int Size =>Columnas * Filas * MapTile.LENGTH;

		public void EndLoad()
		{
			int index;
			int raw;
			MapTile m;

			if (!ReferenceEquals(DataToLoad, default))
            {
				for (int x = 0,xF=Columnas,yF=Filas; x < xF; x++)
				{
					for (int y = 0; y < yF; y++)
					{

						index = (int)((y * yF) + x);
						raw = new Word(DataToLoad, index * Word.LENGTH);
						m = new MapTile((raw & 0x3FF), (raw & 0xFC00) >> 10);
						MapTiles[x, y] = m;

					}
				}
				DataToLoad = default;
            }
        }
		public Collage GetCollage(Tileset tileset,GranPaleta paleta=default)
		{
			Collage collage = new Collage();

			EndLoad();

			if (Equals(paleta, default))
				paleta = tileset.Paletas.FirstOrDefault();
			if (Equals(paleta, default))
				throw new ArgumentNullException(nameof(paleta));

			for (int y = 0, xF = Columnas, yF = Filas; y < yF; y++)
				for (int x = 0; x < xF; x++)
					collage.Add(tileset.Get(Get(x,y).ID).Get(paleta), x*Tile.LADO, y * Tile.LADO);

			return collage;

		}
		public Bitmap GetBitmap(Tileset tileset, GranPaleta paleta = default)=> GetCollage(tileset, paleta).CrearCollage();

		public MapTile Get(int x, int y)
		{
			MapTile tile;

			EndLoad();

			if (x < 0 && y < 0)
				tile = MapTiles[0, 0];
			else if (x < 0)
				tile = MapTiles[0, y];
			else if (y < 0)
				tile = MapTiles[x, 0];
			else if (x >=Columnas && y >= Filas)
				tile = MapTiles[Columnas-1, Filas-1];
			else if (x >= Columnas)
				tile = MapTiles[Columnas-1, y];
			else if (y >= Filas)
				tile = MapTiles[x, Filas-1];
			else tile = MapTiles[x, y];

			return tile;
		}

		public MapTile[,] Get(int x, int y, int width, int height)
		{
			MapTile[,] mapTiles = new MapTile[width, height];
			EndLoad();
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
			EndLoad();
			for (int x = 0, xOld = Columnas, yOld = Filas; x < xSize; x++)
				for (int y = 0; y < ySize; y++)
				{
					if (x < xOld && y < yOld)
						newMapTiles[x, y] = MapTiles[x, y];
					else
						newMapTiles[x, y] = new MapTile();

				}

			MapTiles = newMapTiles;
		}
		public static MapTileData Get(RomGba rom, MapData mData,int offsetMapTiles)
		{

			MapTileData mapTileData = new MapTileData();
			mapTileData.MapTiles = new MapTile[mData.Width,mData.Height];

			if (!Equals(offsetMapTiles, default))
			{
				mapTileData.DataToLoad = rom.Data.SubArray(offsetMapTiles, (int)(mData.Width * mData.Height * Word.LENGTH));
			}
			else throw new ArgumentException("se requiere el offset para cargar!",nameof(offsetMapTiles));
			return mapTileData;
		}
	}

}
