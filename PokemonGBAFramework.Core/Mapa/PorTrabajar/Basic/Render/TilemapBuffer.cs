using Gabriel.Cat.S.Drawing;
using Gabriel.Cat.S.Extension;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic.Render
{
	public class TilemapBuffer
	{
		public bool EsUnaPaletaDe256;
		public Collage collage;
		public Bitmap imgBuffer;
		public BloqueImagen rawImage;
		private Bitmap imagen;
		private byte[] dcmpTilemap;
		private Bitmap[] imagenes;
		public int SelectedMap;
		private bool RefreshMap;
		private Bitmap renderedMap;
		public Paleta colores;//8bpp colors
		public Paleta[] paletas;//4bpp colors
		public int mapnum;


		public TilemapBuffer(Paleta[] colors, byte[] Tilemap, byte[] GFX)
		{
			RefreshMap = true;
			Load4BPP(colors, Tilemap, GFX);


		}
		public TilemapBuffer(Paleta colors, byte[] Tilemap, byte[] GFX)
		{
			Load8BPP(colors, Tilemap, GFX);


		}
		public Bitmap GetRenderedMap()
		{
			if (RefreshMap)
			{
				DrawMap();
				RefreshMap = false;
			}
			return renderedMap;
		}
		public Bitmap GetImage(int pal)
		{
			return imagenes[pal];
		}
		public Bitmap Imagen => imagen;


		public void SetTile(int x, int y, int map)
		{
			SelectedMap = map;
			SetTile(x, y);
		}
		public void SetTile(int x, int y)
		{
			RefreshMap = true;
			SetTile(x, y);

		}

		private void Draw4BPP()
		{
			Bitmap img;
			int tileX;
			int tileY;
			int posInMap;
			int val;
			int curtile;
			int pal;
			bool hf;
			bool hv;
			//int len = (dcmpTilemap.Length);We're 8bpp now just trying to get the regular tiles to display

			int[] tiles = new int[dcmpTilemap.Length / 2];
			byte[] special = new byte[dcmpTilemap.Length / 2];
			int counter = 0;
			for (int i = 0,f= dcmpTilemap.Length / 2; i < f; i++)
			{

				tiles[i] = (dcmpTilemap[counter] & 0xFF) + ((dcmpTilemap[counter + 1] & 0xFF) << 8);
				special[i] = dcmpTilemap[counter + 1];
				counter += 2;
			}
			collage.RemoveAll();
			for (int i = 0; i < tiles.Length; i++)
			{
				tileX = (i % 30);
				tileY = (i / 30);
				//img = new Bitmap(tile_x * 8, tile_y * 8);
				//img.CambiarPixel(Color.Transparent, Color.Red);
				//collage.Add(img, 8, 8);
				try
				{
					 posInMap = 0x0;
					 val = tiles[(byte)posInMap + i] + (special[(byte)posInMap + i] << 8);
					 curtile = tiles[posInMap + i] & 0x3FF;
					 pal = (special[(byte)posInMap + i] & 0xF0) >> 4;
					 hf = (special[(byte)posInMap + i] & 0x4) == 4;
					 hv = (special[(byte)posInMap + i] & 0x8) == 8;

					Console.WriteLine(String.Format("%05x", i * 2) + " " +
							String.Format("%04x", tileX) + " " +
							String.Format("%04x", tileY) + " " +
							String.Format("%04x", val) + " " +
							String.Format("%04x", (pal)) + " " +
							String.Format("%04x", (curtile)) + " " +
									  hf + " " + hv);


					collage.Add(get4BPPTile(curtile, pal, hf, hv),
							tileX * 8, tileY * 8);
				}
				catch { }
			}

		}

		public void Draw8BPP()
		{
			int tileX;
			int tileY;
			int srcX;
			int srcY;
			int kX;
			int kY;
			int kZ;

			collage.RemoveAll();
			for (tileY = 0; tileY < 32; tileY++)
			{

				for (tileX = 0; tileX < 32; tileX++)
				{




					srcX = (tileX) * 8;
					srcY = (tileY) * 8;

					kX = tileX;
					kY = (tileY) * 64;
					kZ = 0x000;
					collage.Add(get8BPPTile(dcmpTilemap[kX + kY + kZ] & 0xFF),
									srcX,
									srcY);
				}
			}

		}
		public void DrawMap()
		{
			if (EsUnaPaletaDe256)

				Draw8BPP();
			else
				Draw4BPP();
		}
		public void RenderBufferGFX()
		{
			if (EsUnaPaletaDe256)
			{

				rawImage = new BloqueImagen(rawImage.DatosDescomprimidos, colores);//, new Point(512, 512));//pntSz);

				imagen = rawImage;
			}
			else
			{

				for (int i = 0; i < paletas.Length; i++)
				{
					imagenes[i] = rawImage + paletas[i];
				}

			}

		}

		void Load4BPP( Paleta[] colors, byte[] Tilemap, byte[] datosImgDescomprimidos)
		{

			EsUnaPaletaDe256 = false;
			paletas = colors;
			dcmpTilemap = Tilemap;
			imagenes = new Bitmap[6];
			rawImage = new BloqueImagen(new BloqueBytes(datosImgDescomprimidos), paletas[0]);//, new Point(512, 512));//pntSz);	
			RenderBufferGFX();
			initGCBuff();

		}


		void Load8BPP( Paleta colors, byte[] Tilemap, byte[] datosImgDescomprimidos)
		{
			EsUnaPaletaDe256 = true;
			colores = colors;
			dcmpTilemap = Tilemap;
			rawImage = new BloqueImagen(new BloqueBytes(datosImgDescomprimidos), colores);//, new Point(512, 512));//pntSz);	
			imagen = rawImage;
			RenderBufferGFX();
			initGCBuff();

		}




		private void initGCBuff()
		{
			imgBuffer = new Bitmap(1024, 1024);
		}

		public int GetTile(int x, int y, int map)
		{

			SelectedMap = map;
			return GetTile(x, y);
		}
		public int GetTile(int x, int y)
		{


			return 0;//For now D;
		}
		//Fit the following into GBAUtils eventually....
		public Bitmap get8BPPTile(int tileNum)
		{
			Bitmap toSend;

			int x = ((tileNum) % (64)) * 8;
			int y = ((tileNum) / (64)) * 8;
			
			try
			{
				toSend = Imagen.Recortar(new Point(x, y),new Size( 8, 8));
			}
			catch
			{
				toSend = new Bitmap(8, 8);
			}


			return toSend;
		}
		public Bitmap get4BPPTile(long tile)
		{
			return get4BPPTile((int)tile & 0x3FF, (int)((tile & 0xF000) >> 12), (tile & 0x400) == 0x400, (tile & 0x800) == 0x800);
		}
		public Bitmap get4BPPTile(long tileNum, long palette, bool xFlip, bool yFlip)
		{
			Bitmap toSend;

			int x = (int)(((tileNum & 0x3FF) % (32)) * 8);
			int y = (int)(((tileNum & 0x3FF) / (32)) * 8);
			
			try
			{
				toSend = GetImage((int)palette).Recortar(new Point(x, y),new Size( 8, 8));
				toSend = toSend.Flip(xFlip, yFlip);
			}
			catch
			{
				toSend = new Bitmap(8, 8);
			}



			return toSend;
		}

	}

}
