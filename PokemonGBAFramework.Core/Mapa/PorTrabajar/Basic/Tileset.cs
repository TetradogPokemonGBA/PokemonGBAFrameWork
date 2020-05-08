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
		private RomGba rom;
		private BloqueImagen image;
		private Bitmap[,] bi;
		private Paleta[,] palettes;
		private Paleta[,] palettesFromROM;
		private static Tileset lastPrimary;
		public TilesetHeader tilesetHeader;

		public const int numBlocks = 1024; //(tilesetHeader.isPrimary ? DataStore.MainTSBlocks : DataStore.LocalTSBlocks); //INI RSE=0x207 : 0x88, FR=0x280 : 0x56

		private SortedList<int, Bitmap>[] renderedTiles;
		private SortedList<int, Bitmap>[] customRenderedTiles;
		private static readonly byte[] localTSLZHeader = new byte[] { 10, 80, 9, 00, 32, 00, 00 };
		private static readonly byte[] globalTSLZHeader = new byte[] { 10, 80, 9, 00, 32, 00, 00 };

		public bool modified = false;

		public Tileset(RomGba rom, int offset, int mainTSHeight, int localTSHeight)
		{
			this.rom = rom;
			loadData(offset);
			renderTiles(mainTSHeight,localTSHeight);
		}

		public void loadData(int offset)
		{
			tilesetHeader = new TilesetHeader(rom, offset);
		}

		public void renderGraphics(int mainTSHeight,int localTSHeight)
		{
			RomGba backup;
			byte[] uncompressedData;
			int imageDataPtr = (int)tilesetHeader.PGFX;

			if (tilesetHeader.IsPrimary)
				lastPrimary = this;
			

			if (tilesetHeader.BCompressed == 1)
				uncompressedData = LZ77.Descomprimir(rom.Data.Bytes, imageDataPtr);
			else
			{
				backup = new RomGba((byte[])rom.Data.Bytes.Clone()); //Backup in case repairs fail
				rom.Data.SetArray((int)tilesetHeader.PGFX, (tilesetHeader.IsPrimary ? globalTSLZHeader : localTSLZHeader)); //Attempt to repair the LZ77 data
				uncompressedData = LZ77.Descomprimir(rom.Data.Bytes, imageDataPtr);
				rom = new RomGba((byte[])backup.Data.Bytes.Clone()); //TODO add dialog to allow repairs to be permanant
				if (uncompressedData == null) //If repairs didn't go well, revert ROM and pull uncompressed data
				{
					uncompressedData = rom.Data.SubArray(imageDataPtr, (tilesetHeader.IsPrimary ? 128 * mainTSHeight : 128 * localTSHeight) / 2); //TODO: Hardcoded to FR tileset sizes
				}
			}

			renderedTiles = new SortedList<int, Bitmap>[16 * 4];
			customRenderedTiles = new SortedList<int, Bitmap>[16 * 4];

			for (int i = 0; i < 16 * 4; i++)
				renderedTiles[i] = new SortedList<int, Bitmap>();
			for (int i = 0; i < 16 * 4; i++)
				customRenderedTiles[i] = new SortedList<int, Bitmap>();

			image = new BloqueImagen() { DatosDescomprimidos = new BloqueBytes(uncompressedData) };
			image.Paletas.Add(palettes[0,0]);//, new Point(128, (tilesetHeader.isPrimary ? DataStore.MainTSHeight : DataStore.LocalTSHeight)));
		}

		public void renderPalettes()
		{
			palettes = new Paleta[4,16];
			bi = new Bitmap[4,16];

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					palettes[i,j] =  Paleta.Get(rom.Data.SubArray(((int)tilesetHeader.PPalettes) + ((32 * j) + (i * 0x200)), 32));
				}
			}
			palettesFromROM = palettes;
		}

		public void renderTiles(int mainTSHeight, int localTSHeight)
		{
			renderPalettes();
			renderGraphics(mainTSHeight,localTSHeight);
		}

		//public void startTileThreads()
		//{
		//	for (int i = 0; i < (tilesetHeader.IsPrimary ? DataStore.MainTSPalCount : 13); i++)
		//		new TileLoader(renderedTiles, i).start();
		//}

		public Bitmap getTileWithCustomPal(int tileNum, Paleta palette, bool xFlip, bool yFlip, int time)
		{
			int x = ((tileNum) % (bi[time,0].Width / 8)) * 8;
			int y = ((tileNum) / (bi[time,0].Width / 8)) * 8;
			Bitmap toSend = (image+palette).Recortar(new Point(x, y),new Size( 8, 8));

			if (!xFlip && !yFlip)//no tiene sentido porque si no son false los dos entrara en un if y si lo son en el return ira lo mismo...
				return toSend;
			if (xFlip)
				toSend = horizontalFlip(toSend);
			if (yFlip)
				toSend = verticalFlip(toSend);

			return toSend;
		}

		public Bitmap getTile(int tileNum, int palette, bool xFlip, bool yFlip, int time,int mainTSPalCount)
		{
			if (palette < mainTSPalCount)
			{
				if (renderedTiles[palette + (time * 16)].ContainsKey(tileNum)) //Check to see if we've cached that tile
				{
					if (xFlip && yFlip)
						return verticalFlip(horizontalFlip(renderedTiles[palette + (time * 16)][tileNum]));
					else if (xFlip)
					{
						return horizontalFlip(renderedTiles[palette + (time * 16)][tileNum]);
					}
					else if (yFlip)
					{
						return verticalFlip(renderedTiles[palette + (time * 16)][tileNum]);
					}

					return renderedTiles[palette + (time * 16)][tileNum];
				}
			}
			else if (palette < 13)
			{
				if (customRenderedTiles[(palette - mainTSPalCount) + (time * 16)].ContainsKey(tileNum)) //Check to see if we've cached that tile
				{
					if (xFlip && yFlip)
						return verticalFlip(horizontalFlip(customRenderedTiles[(palette - mainTSPalCount) + (time * 16)][tileNum]));
					else if (xFlip)
					{
						return horizontalFlip(customRenderedTiles[(palette - mainTSPalCount) + (time * 16)][tileNum]);
					}
					else if (yFlip)
					{
						return verticalFlip(customRenderedTiles[(palette - mainTSPalCount) + (time * 16)][tileNum]);
					}

					return customRenderedTiles[(palette - mainTSPalCount) + (time * 16)][tileNum];
				}
			}
			else
			{
				//	System.out.println("Attempted to read tile " + tileNum + " of palette " + palette + " in " + (tilesetHeader.isPrimary ? "global" : "local") + " tileset!");
				return new Bitmap(8, 8);
			}

			int x = ((tileNum) % (128 / 8)) * 8;
			int y = ((tileNum) / (128 / 8)) * 8;
			Bitmap toSend = new Bitmap(8, 8);
			try
			{
				toSend = bi[time,palette].Recortar(new Point(x, y),new Size( 8, 8));
			}
			catch (Exception e)
			{
				//e.printStackTrace();
				//	System.out.println("Attempted to read 8x8 at " + x + ", " + y);
			}
			if (palette < mainTSPalCount || renderedTiles.Length > mainTSPalCount)
				renderedTiles[palette + (time * 16)].Add(tileNum, toSend);
			else
				customRenderedTiles[(palette - mainTSPalCount) + (time * 16)].Add(tileNum, toSend);

			if (!xFlip && !yFlip)
				return toSend;
			if (xFlip)
				toSend = horizontalFlip(toSend);
			if (yFlip)
				toSend = verticalFlip(toSend);

			return toSend;
		}

		public Paleta[] getPalette(int time)
		{
			return palettes.GetFila(time);
		}

		public Paleta[,] getROMPalette()
		{
			return (Paleta[,])palettesFromROM.Clone(); //No touchy the real palette!
		}

		public void resetPalettes()
		{
			palettes = getROMPalette();
		}

		public void setPalette(Paleta[] pal, int time)
		{
			palettes.SetFila(time,pal);
		}

		public void setPalette(Paleta pal, int index, int time)
		{
			palettes[time,index] = pal;
		}

		public void rerenderTileSet(int palette, int time)
		{
			bi[time,palette] = image[palettes[time,palette]];
		}

		public void renderPalettedTiles()
		{
			for (int j = 0; j < 4; j++)
			{
				for (int i = 0; i < 16; i++)
				{
					bi[j,i] = image + (palettes[j,i]);

				}
			}
			for (int j = 0; j < 4; j++)
				for (int i = 0; i < 16; i++)
					rerenderTileSet(i, j);
		}
		public void resetCustomTiles()
		{
			customRenderedTiles = new SortedList<int, Bitmap>[16 * 4];
			for (int i = 0; i < 16 * 4; i++)
				customRenderedTiles[i] = new SortedList<int, Bitmap>();
		}

		private Bitmap horizontalFlip(Bitmap img)
		{
			Bitmap dimg = (Bitmap)img.Clone();
			dimg.RotateFlip(RotateFlipType.RotateNoneFlipX);//mirar que sea asi
			return dimg;
		}

		private Bitmap verticalFlip(Bitmap img)
		{
			Bitmap dimg = (Bitmap)img.Clone();
			dimg.RotateFlip(RotateFlipType.RotateNoneFlipY);//mirar que sea asi
			return dimg;
		}

		public Bitmap getTileSet(int palette, int time)
		{
			return bi[time,palette];
		}

		public Bitmap getIndexedTileSet(int palette, int time)
		{
			return image + (palettes[time,palette]);//true
		}

		public TilesetHeader getTilesetHeader()
		{
			return tilesetHeader;
		}


		private class TileLoader
		{
			SortedList<int, Bitmap>[]
			buffer;
			int pal;
			public TileLoader(SortedList<int, Bitmap>[] hash, int palette)
			{
				buffer = hash;
				pal = palette;
			}


			public void run(Tileset tileset,int mainTSSize,int localTSSize,int mainTSPalCount)
			{
				int k = tileset.getTilesetHeader().IsPrimary ? mainTSSize : localTSSize;

				for (int i = 0; i < 1023; i++)
				{
					try
					{
						buffer[pal].Add(i, tileset.getTile(i, pal, false, false, 0,mainTSPalCount));
					}
					catch (Exception e)
					{
						// e.printStackTrace();
						Console.WriteLine("An error occured while writing tile " + i + " with palette " + pal);
					}
				}

			}

		}

		//public void Save()
		//{
		//	//for (int j = 0; j < 1; j++) //Caused issues last time I tested it...
		//	//{
		//	//	for (int i = 0; i < (tilesetHeader.isPrimary ? DataStore.MainTSPalCount : 16); i++)
		//	//	{
		//	//		posicion en la rom donde va la paleta (((int)tilesetHeader.pPalettes) + (32 * i + (j * 0x200)));
		//	//		palettes[j][i].save(rom);
		//	//	}
		//	//}
		//	//tilesetHeader.save();
		//}
	}
}
