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
		class TileLoader
		{
			SortedList<int, Bitmap>[] buffer;
			int paleta;
			public TileLoader(SortedList<int, Bitmap>[] hash, int palette)
			{
				buffer = hash;
				paleta = palette;
			}


			public void run(Tileset tileset, int mainTSSize, int localTSSize, int mainTSPalCount)
			{
				int k = tileset.TilesetHeader.IsPrimary ? mainTSSize : localTSSize;

				for (int i = 0; i < 1023; i++)
				{
					try
					{
						buffer[paleta].Add(i, tileset.Get(i, paleta, false, false, 0, mainTSPalCount));
					}
					catch (Exception e)
					{
						// e.printStackTrace();
						Console.WriteLine("An error occured while writing tile " + i + " with palette " + paleta);
					}
				}

			}

		}

		public const int NUMBLOCKS = 1024; //(tilesetHeader.isPrimary ? DataStore.MainTSBlocks : DataStore.LocalTSBlocks); //INI RSE=0x207 : 0x88, FR=0x280 : 0x56
		public const int MAXTIME =4;
		public const int MAXFILA = 16;
		public const int HEIGHT = Tile.LADO * MAXFILA;
		public const int WIDTH = Tile.LADO * MAXTIME;

		private static readonly byte[] localTSLZHeader = new byte[] { 10, 80, 9, 00, 32, 00, 00 };
		private static readonly byte[] globalTSLZHeader = new byte[] { 10, 80, 9, 00, 32, 00, 00 };

		private BloqueImagen image;
		private Bitmap[,] tileset;
		private Paleta[,] paletas;
		private Paleta[,] paletasOriginales;
		private SortedList<int, Bitmap>[] renderedTiles;
		private SortedList<int, Bitmap>[] customRenderedTiles;

		public Tileset(RomGba rom, int offset, int mainTSHeight, int localTSHeight)
		{
			loadData(rom,offset);
			renderTiles(rom,mainTSHeight,localTSHeight);
		}

		public TilesetHeader TilesetHeader { get; private set; }
		public static Tileset LastPrimary { get; set; }
		public bool Modified { get; set; } = false;

		public void loadData(RomGba rom,int offset)
		{
			TilesetHeader = new TilesetHeader(rom, offset);
		}

		public void renderGraphics(RomGba rom, int mainTSHeight,int localTSHeight)
		{
			RomGba backup;
			byte[] uncompressedData;
			int imageDataPtr = (int)TilesetHeader.PGFX;

			if (TilesetHeader.IsPrimary)
				LastPrimary = this;
			

			if (TilesetHeader.BCompressed == 1)
				uncompressedData = LZ77.Descomprimir(rom.Data.Bytes, imageDataPtr);
			else
			{
				backup = new RomGba((byte[])rom.Data.Bytes.Clone()); //Backup in case repairs fail
				rom.Data.SetArray((int)TilesetHeader.PGFX, (TilesetHeader.IsPrimary ? globalTSLZHeader : localTSLZHeader)); //Attempt to repair the LZ77 data
				uncompressedData = LZ77.Descomprimir(rom.Data.Bytes, imageDataPtr);
				rom = new RomGba((byte[])backup.Data.Bytes.Clone()); //TODO add dialog to allow repairs to be permanant
				if (uncompressedData == null) //If repairs didn't go well, revert ROM and pull uncompressed data
				{
					uncompressedData = rom.Data.SubArray(imageDataPtr, (TilesetHeader.IsPrimary ? 128 * mainTSHeight : 128 * localTSHeight) / 2); //TODO: Hardcoded to FR tileset sizes
				}
			}

			renderedTiles = new SortedList<int, Bitmap>[16 * 4];
			customRenderedTiles = new SortedList<int, Bitmap>[16 * 4];

			for (int i = 0; i < 16 * 4; i++)
				renderedTiles[i] = new SortedList<int, Bitmap>();
			for (int i = 0; i < 16 * 4; i++)
				customRenderedTiles[i] = new SortedList<int, Bitmap>();

			image = new BloqueImagen() { DatosDescomprimidos = new BloqueBytes(uncompressedData) };
			image.Paletas.Add(paletas[0,0]);//, new Point(128, (tilesetHeader.isPrimary ? DataStore.MainTSHeight : DataStore.LocalTSHeight)));
		}

		public void LoadPaletas(RomGba rom)
		{
			const int LENGTHFILA = 0x200;

			paletas = new Paleta[MAXTIME, MAXFILA];
			tileset = new Bitmap[MAXTIME, MAXFILA];

			for (int i = 0; i < MAXTIME; i++)
			{
				for (int j = 0; j < MAXFILA; j++)
				{
					paletas[i,j] =  Paleta.Get(rom,(int)TilesetHeader.OffsetPaletas + ((Paleta.LENGTH * j) + (i * LENGTHFILA)));
				}
			}
			paletasOriginales = (Paleta[,])paletas.Clone();
		}

		public void renderTiles(RomGba rom, int mainTSHeight, int localTSHeight)
		{
			LoadPaletas(rom);
			renderGraphics(rom,mainTSHeight,localTSHeight);
		}



		public Bitmap Get(int tileNum, Paleta palette, bool xFlip, bool yFlip, int time)
		{
			const int FIRST = 0;

			int x = ((tileNum) % (tileset[time, FIRST].Width / Tile.LADO)) * Tile.LADO;
			int y = ((tileNum) / (tileset[time, FIRST].Width / Tile.LADO)) * Tile.LADO;
			Bitmap toSend = (image+palette).Recortar(new Point(x, y),new Size( Tile.LADO, Tile.LADO));
			
			if (xFlip)
				toSend = toSend.HorizontalFlip();
			if (yFlip)
				toSend = toSend.VerticalFlip();

			return toSend;
		}

		public Bitmap Get(int tileNum, int palette, bool xFlip, bool yFlip, int time,int totalPaletas)
		{
			
			int x;
			int y;
			Bitmap toSend=default;
			if (palette < totalPaletas)
			{
				if (renderedTiles[palette + (time * MAXFILA)].ContainsKey(tileNum)) //Check to see if we've cached that tile
				{
					toSend = renderedTiles[palette + (time * 16)][tileNum];
				}
			}
			else if (palette < 13)
			{
				if (customRenderedTiles[(palette - totalPaletas) + (time * 16)].ContainsKey(tileNum)) //Check to see if we've cached that tile
				{
					toSend = customRenderedTiles[(palette - totalPaletas) + (time * 16)][tileNum];

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
				toSend = new Bitmap(Tile.LADO, Tile.LADO);
				try
				{
					toSend = tileset[time, palette].Recortar(new Point(x, y), new Size(Tile.LADO, Tile.LADO));
				}
				catch
				{
					Console.WriteLine($"Attempted to read {Tile.LADO}x{Tile.LADO} at {x},{y}");
				}
				if (palette < totalPaletas || renderedTiles.Length > totalPaletas)
					renderedTiles[palette + (time * MAXFILA)].Add(tileNum, toSend);
				else
					customRenderedTiles[(palette - totalPaletas) + (time * 16)].Add(tileNum, toSend);

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

		public void SetPaletas(int time, Paleta[] pal)
        {
			paletas.SetFila(time,pal);
		}

		public void SetPaleta(int time, int index, Paleta pal)
        {
			paletas[time,index] = pal;
		}

		public void Refresh(int time, int palette)
        {
			tileset[time,palette] = image[paletas[time,palette]];
		}

		public void Refresh()
		{
			for (int t = 0; t < MAXTIME; t++)
			{
				for (int f = 0; f < MAXFILA; f++)
				{
					tileset[t,f] = image + (paletas[t,f]);

				}
			}
			for (int t = 0; t < MAXTIME; t++)
				for (int f = 0; f < MAXFILA; f++)
					Refresh(t, f);
		}
		public void resetCustomTiles()
		{
			customRenderedTiles = new SortedList<int, Bitmap>[MAXFILA * MAXTIME];
			for (int i = 0,f = MAXFILA * MAXTIME; i <f; i++)
				customRenderedTiles[i] = new SortedList<int, Bitmap>();
		}



		public Bitmap getTileSet(int time, int palette)
        {
			return tileset[time,palette];
		}

		public Bitmap GetIndexedTileSet(int time, int paleta)
        {
			return image + (paletas[time,paleta]);//true
		}







	}
}
