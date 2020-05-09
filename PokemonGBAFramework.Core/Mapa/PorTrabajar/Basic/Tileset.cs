using Gabriel.Cat.S.Extension;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Tileset
	{

		public const int NUMBLOCKS = 1024; //(tilesetHeader.isPrimary ? DataStore.MainTSBlocks : DataStore.LocalTSBlocks); //INI RSE=0x207 : 0x88, FR=0x280 : 0x56
		public const int MAXTIME =4;
		public const int MAXFILA = 16;
		public const int HEIGHT = Tile.LADO * MAXFILA;
		public const int WIDTH = Tile.LADO * MAXTIME;
		public static Tileset LastPrimary { get; set; }

		BloqueImagen imagen;
		private Bitmap[,] tileset;
		private Paleta[,] paletas;
		private Paleta[,] paletasOriginales;
		SortedList<int, SortedList<int, Bitmap>> tilesHechas = new SortedList<int, SortedList<int, Bitmap>>();
		public TilesetHeader TilesetHeader { get; private set; }

		public bool Modified { get; set; } = false;



		public Bitmap Get(int tileNum, Paleta palette, bool xFlip, bool yFlip, int time)
		{
			const int FIRST = 0;

			int x = ((tileNum) % (tileset[time, FIRST].Width / Tile.LADO)) * Tile.LADO;
			int y = ((tileNum) / (tileset[time, FIRST].Width / Tile.LADO)) * Tile.LADO;
			Bitmap toSend = (imagen+palette).Recortar(new Point(x, y),new Size( Tile.LADO, Tile.LADO));
			
			if (xFlip)
				toSend = toSend.HorizontalFlip();
			if (yFlip)
				toSend = toSend.VerticalFlip();

			return toSend;
		}

		public Bitmap Get(RomGba rom, Tile tile,int time) => Get(rom,tile.IndexTile, tile.IndexPaleta, tile.XFlip, tile.YFlip,time);
		public Bitmap Get(RomGba rom,int tileNum, int palette, bool xFlip, bool yFlip, int time)
		{
			int index;
			int x;
			int y;
			Bitmap toSend=default;
			int totalPaletas = TilesetHeader.GetPaletaCount(rom);
			if (palette < totalPaletas)
			{
				index = palette + (time * MAXFILA);
				if (tilesHechas.ContainsKey(index)&&tilesHechas[index].ContainsKey(tileNum)) //Check to see if we've cached that tile
				{
					toSend = tilesHechas[index][tileNum];
				}
			}
			else if (palette < 13)
			{
				index = (palette - totalPaletas) + (time * 16);
				if (tilesHechas.ContainsKey(index) && tilesHechas[index].ContainsKey(tileNum)) //Check to see if we've cached that tile
				{
					toSend = tilesHechas[index][tileNum];

				}
			}
			else
			{
				toSend = new Bitmap(Tile.LADO, Tile.LADO);
			}


			if (Equals(toSend, default))
			{
				x = ((tileNum) % (HEIGHT / Tile.LADO)) * Tile.LADO;
				y = ((tileNum) / (HEIGHT / Tile.LADO)) * Tile.LADO;


				if (tileset[time, palette] != default)
					toSend = tileset[time, palette].Recortar(new Point(x, y), new Size(Tile.LADO, Tile.LADO));
				else
				{
					toSend = new Bitmap(Tile.LADO, Tile.LADO);
					Console.WriteLine($"Attempted to read {Tile.LADO}x{Tile.LADO} at {x},{y}");
				}
				index = palette + (time * MAXFILA);

				if (palette > totalPaletas)
					index -= totalPaletas;
				if (!tilesHechas.ContainsKey(index))
					tilesHechas.Add(index, new SortedList<int, Bitmap>());
				tilesHechas[index].Add(tileNum, toSend);

			}

			if (xFlip)
			{
				toSend = toSend.HorizontalFlip();
			}
			if (yFlip)
			{
				toSend = toSend.VerticalFlip();
			}

			return toSend;
		}

		public Paleta[] GetPaletas(int time)
		{
			return paletas.GetFila(time);
		}

		public Paleta[,] GetTodasLasPaletas()
		{
			return (Paleta[,])paletasOriginales.Clone(); //No touchy the real palette!
		}

		public void RestaurarPaletas()
		{
			paletas = GetTodasLasPaletas();
		}

		public void SetPaletas(int time, Paleta[] paletas)
        {
			this.paletas.SetFila(time, paletas);
		}

		public void SetPaleta(int time, int index, Paleta paleta)
        {
			paletas[time,index] = paleta;
		}

		public void Refresh(int time, int palette)
        {
			tileset[time,palette] = imagen+paletas[time,palette];
		}

		public void Refresh()
		{
			for (int t = 0; t < MAXTIME; t++)
			{
				for (int f = 0; f < MAXFILA; f++)
				{
					tileset[t,f] = imagen + (paletas[t,f]);

				}
			}
			for (int t = 0; t < MAXTIME; t++)
				for (int f = 0; f < MAXFILA; f++)
					Refresh(t, f);
		}


		public Bitmap getTileSet(int time, int palette)
        {
			return tileset[time,palette];
		}

		public Bitmap GetIndexedTileSet(int time, int paleta)
        {
			return imagen + (paletas[time,paleta]);
		}


		public static Tileset Get(RomGba rom,int offset)
		{
			return Get(rom,TilesetHeader.Get(rom, offset));
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
			if (uncompressedData == null) //Attempt to repair the LZ77 data
			{
				oldHeader = rom.Data.SubArray(tileset.TilesetHeader.OffsetImagen, TilesetHeader.HeaderFix.Length);
				rom.Data.SetArray(tileset.TilesetHeader.OffsetImagen, TilesetHeader.HeaderFix);
				uncompressedData = LZ77.Descomprimir(rom.Data.Bytes, imageDataPtr, false);
				if (uncompressedData == null)//If repairs didn't go well, revert ROM and pull uncompressed data
				{
					rom.Data.SetArray(tileset.TilesetHeader.OffsetImagen, oldHeader);//lo pongo como estaba
					uncompressedData = rom.Data.SubArray(imageDataPtr, (tileset.TilesetHeader.IsPrimary ? 128 * TilesetHeader.GetMainHeight(rom) : 128 * TilesetHeader.GetLocalHeight(rom)) / 2); //TODO: Hardcoded to FR tileset sizes

				}

			}

			tileset.imagen = new BloqueImagen() { DatosDescomprimidos = new BloqueBytes(uncompressedData) };

			tileset.paletasOriginales = GetPaletas(rom, tileset.TilesetHeader.OffsetPaletas);
			tileset.paletas = (Paleta[,])tileset.paletasOriginales.Clone();

			tileset.imagen.Paletas.Add(tileset.paletas[0, 0]);

			return tileset;
		}



		public static Paleta[,] GetPaletas(RomGba rom, int offsetPaletas)
		{
			int offset = offsetPaletas;
			Paleta[,] paletas = new Paleta[MAXTIME, MAXFILA];

			for (int x = 0; x < MAXTIME; x++)
			{
				for (int y = 0; y < MAXFILA; y++)
				{
					paletas[x, y] = Paleta.Get(rom, offset);
					offset += Paleta.LENGTH;
				}
			}
			return paletas;
		}




	}
}
