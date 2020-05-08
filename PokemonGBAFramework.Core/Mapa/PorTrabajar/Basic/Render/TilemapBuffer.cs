using Gabriel.Cat.S.Drawing;
using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic.Render
{
	public class TilemapBuffer
	{
		public int myType;
		public Collage collage;
		public Bitmap imgBuffer;
		public BloqueImagen rawImage;
		private Bitmap bi;
		private byte[] dcmpTilemap;
		private Bitmap[] mybi;
		public bool bLoaded;
		public int SelectedMap;
		private bool RefreshMap;
		private Bitmap renderedMap;
		public Paleta p;//8bpp colors
		public Paleta[] myPal;//4bpp colors
		public byte[] dcmpGFX;
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
		public Bitmap GetImage(int Pal)
		{
			return mybi[Pal];
		}
		public Bitmap GetImage()
		{


			return bi;

			//DOES NOT SET
		}


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
			int i = 0;
			int len = (dcmpTilemap.Length);//We're 8bpp now just trying to get the regular tiles to display
			i = 0;
			int tile_x = 0;
			int tile_y = 0;
			//This is gonna be dumb...
			i = 0;
			int[] tiles = new int[dcmpTilemap.Length / 2];
			byte[] special = new byte[dcmpTilemap.Length / 2];
			int counter = 0;
			for (i = 0; i < len / 2; i++)
			{

				tiles[i] = (dcmpTilemap[counter] & 0xFF) + ((dcmpTilemap[counter + 1] & 0xFF) << 8);
				special[i] = dcmpTilemap[counter + 1];
				counter += 2;
			}
			collage.RemoveAll();
			for (i = 0; i < tiles.Length; i++)
			{
				tile_x = (i % 30);
				tile_y = (i / 30);
				//img = new Bitmap(tile_x * 8, tile_y * 8);
				//img.CambiarPixel(Color.Transparent, Color.Red);
				//collage.Add(img, 8, 8);
				try
				{
					int posInMap = 0x0;
					int val = tiles[(byte)posInMap + i] + (special[(byte)posInMap + i] << 8);
					int curtile = tiles[posInMap + i] & 0x3FF;
					int pal = (special[(byte)posInMap + i] & 0xF0) >> 4;
					bool hf = (special[(byte)posInMap + i] & 0x4) == 4;
					bool hv = (special[(byte)posInMap + i] & 0x8) == 8;

					Console.WriteLine(String.Format("%05x", i * 2) + " " +
							String.Format("%04x", tile_x) + " " +
							String.Format("%04x", tile_y) + " " +
							String.Format("%04x", val) + " " +
							String.Format("%04x", (pal)) + " " +
							String.Format("%04x", (curtile)) + " " +
									  hf + " " + hv);


					collage.Add(get4BPPTile(curtile, pal, hf, hv),
							tile_x * 8, tile_y * 8);
				}
				catch (Exception e) { }
			}


			bLoaded = true;

		}

		public void Draw8BPP()
		{
			int tile_x = 0;
			int tile_y = 0;
			collage.RemoveAll();
			for (tile_y = 0; tile_y < 32; tile_y++)
			{

				for (tile_x = 0; tile_x < 32; tile_x++)
				{




					int srcx = (tile_x) * 8;
					int srcy = (tile_y) * 8;

					int kX = tile_x;
					int kY = (tile_y) * 64;
					int kZ = 0x000;
					collage.Add(get8BPPTile(dcmpTilemap[kX + kY + kZ] & 0xFF),
									srcx,
									srcy);
				}
			}
			bLoaded = true;


		}
		public void DrawMap()
		{
			switch (myType)
			{
				case 16:
					Draw4BPP();

					break;


				case 256:

					Draw8BPP();
					break;

			}

		}
		public void RenderBufferGFX()
		{

			switch (myType)
			{
				case 16:
					int i = 0;

					for (i = 0; i < myPal.Length; i++)
					{
						mybi[i] = new BloqueImagen(new BloqueBytes(dcmpGFX), myPal[i]/*, new Point(256, 512)*/)+myPal[i];//false
					}

					break;


				case 256:

					rawImage = new BloqueImagen(new BloqueBytes(dcmpGFX), p);//, new Point(512, 512));//pntSz);

					bi = rawImage;
					break;

			}
		}

		void Load4BPP( Paleta[] colors, byte[] Tilemap, byte[] GFX)
		{
			dcmpGFX = GFX;
			myType = 16;
			myPal = colors;
			dcmpTilemap = Tilemap;
			mybi = new Bitmap[6];
			rawImage = new BloqueImagen(new BloqueBytes(dcmpGFX), myPal[0]);//, new Point(512, 512));//pntSz);	

			int i = 0;
			RenderBufferGFX();
			initGCBuff();

		}


		void Load8BPP( Paleta colors, byte[] Tilemap, byte[] GFX)
		{
			dcmpGFX = GFX;
			myType = 256;
			p = colors;
			dcmpTilemap = Tilemap;
			rawImage = new BloqueImagen(new BloqueBytes(dcmpGFX), p);//, new Point(512, 512));//pntSz);	
			bi = rawImage;
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


			int x = ((tileNum) % (64)) * 8;
			int y = ((tileNum) / (64)) * 8;
			Bitmap toSend = new Bitmap(8, 8);
			try
			{
				toSend = GetImage().Recortar(new Point(x, y),new Size( 8, 8));
			}
			catch (Exception e)
			{
				//e.printStackTrace();
				//System.out.println("Attempted to read 8x8 at " + x + ", " + y);
			}


			return toSend;
		}
		public Bitmap get4BPPTile(long tile)
		{
			return get4BPPTile((int)tile & 0x3FF, (int)((tile & 0xF000) >> 12), (tile & 0x400) == 0x400, (tile & 0x800) == 0x800);
		}
		public Bitmap get4BPPTile(long tileNum, long palette, bool xFlip, bool yFlip)
		{


			int x = (int)(((tileNum & 0x3FF) % (32)) * 8);
			int y = (int)(((tileNum & 0x3FF) / (32)) * 8);
			Bitmap toSend = new Bitmap(8, 8);
			try
			{
				toSend = GetImage((int)palette).Recortar(new Point(x, y),new Size( 8, 8));
			}
			catch (Exception e)
			{
				//e.printStackTrace();
				//	System.out.println("Attempted to read 8x8 at " + x + ", " + y);
			}



			return toSend;
		}

	}

}
