using Gabriel.Cat.S.Extension;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Tileset
	{

		public const int NUMBLOCKS = 1024;
		public const int MAXTIME =4;
		public const int MINTIME = 1;
		public const int MAXFILA = 16;
		public const int HEIGHT = Tile.LADO * MAXFILA;
		public const int WIDTH = Tile.LADO * MAXTIME;
		public static Tileset LastPrimary { get; set; }

		BloqueImagenGrande imagen;

		GranPaleta[] paletasOriginales;


		public TilesetHeader TilesetHeader { get; private set; }

		public bool Modified { get; set; } = false;
		public List<GranPaleta> Paletas => imagen.Paletas;
		public Bitmap this[int index]
		{
			get
			{
				return imagen.Get(Paletas[index]);
			}
		}

	    public BloqueImagenGrande this[int x, int y]
		{
			get
			{
				return imagen.Get(x, y, Tile.LADO,Tile.LADO);
			}
		}

		public GranPaleta[] GetTodasLasPaletas()
		{//así se pueden modificar y no cambian las originales!
			return GranPaleta.GetClone(paletasOriginales);
		}

		public void RestaurarPaletas()
		{
			Paletas.Clear();
			Paletas.AddRange(GetTodasLasPaletas());


		}
		public BloqueImagenGrande Get(int index) => imagen.Get(index, Tile.LADO);
		public Bitmap Get(int index, int indexPaleta) => Get(index, Paletas[indexPaleta]);
		public Bitmap Get(int index, GranPaleta paleta) =>Get(index).Get(paleta);

		public Bitmap Get(int x,int y, GranPaleta paleta) => this[x, y].Get(paleta);


		public static Tileset Get(RomGba rom,OffsetRom offsetTilesetHeader)
		{
			return Get(rom,TilesetHeader.Get(rom, offsetTilesetHeader));
		}
		public static Tileset Get(RomGba rom,TilesetHeader tileSetHeader)
		{
			byte[] oldHeader;
			byte[] uncompressedData;
			int imageDataPtr;
			Tileset tileset = new Tileset();
			tileset.TilesetHeader = tileSetHeader;

			imageDataPtr = tileset.TilesetHeader.OffsetImagen;


			if (tileset.TilesetHeader.IsPrimary)
				LastPrimary = tileset;

			uncompressedData = LZ77.Descomprimir(rom.Data.Bytes, imageDataPtr, false);
			if (uncompressedData == default) //Attempt to repair the LZ77 data
			{
				oldHeader = rom.Data.SubArray(tileset.TilesetHeader.OffsetImagen, TilesetHeader.HeaderFix.Length);
				rom.Data.SetArray(tileset.TilesetHeader.OffsetImagen, TilesetHeader.HeaderFix);//mirar si se puede poner en la clase LZ77 así se arregla cualquier header o es un header para los Tileset...
				uncompressedData = LZ77.Descomprimir(rom.Data.Bytes, imageDataPtr, false);
				if (uncompressedData == default)//If repairs didn't go well, revert ROM and pull uncompressed data
				{
					rom.Data.SetArray(tileset.TilesetHeader.OffsetImagen, oldHeader);//lo pongo como estaba
					uncompressedData = rom.Data.SubArray(imageDataPtr, (tileset.TilesetHeader.IsPrimary ? Tileset.MAXFILA*Tile.LADO * TilesetHeader.GetMainHeight(rom) : Tileset.MAXFILA * Tile.LADO * TilesetHeader.GetLocalHeight(rom)) / 2); //TODO: Hardcoded to FR tileset sizes

				}

			}

			tileset.imagen = new BloqueImagenGrande(uncompressedData,Tile.LADO* MAXFILA);

			tileset.paletasOriginales = GetPaletas(rom, tileset.TilesetHeader.OffsetPaletas);
			tileset.RestaurarPaletas();

			return tileset;
		}



		public static GranPaleta[] GetPaletas(RomGba rom, OffsetRom offsetTablaPaletasTileset)
		{
			return GranPaleta.Get(rom, offsetTablaPaletasTileset, MAXFILA);
		}




	}
}
