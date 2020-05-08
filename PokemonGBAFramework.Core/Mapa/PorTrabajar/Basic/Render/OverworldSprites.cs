using Gabriel.Cat.S.Drawing;
using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class OverworldSprites
	{
		//Rom structure 
		/*
		public long lIndex; //Primary Key in sprite database?
		public  long lSlot;
		public  long iOffTop; //Hotspot//s top area. Normally 16.
		public  int iOffBot; //Hotspot//s bottom area. Normally 32.
		public  int iPal; //Palette to use. Probably contains more than that, only last digit actually matters.
		public  int filler;
		public  long u1; //An unknown pointer.*/
		public int StarterWord;
		public byte iPal;
		public byte sprVald1;
		public byte sprVald2;
		public byte sprVald3;
		public int FrameSize;
		public int width;
		public int height;
		public int oam1;
		public int oam2;
		public long ptr2anim;
		public long ptrSize; //Pointer to unknown data. Determines sprite size.
		public long ptrAnim; //Pointer to unknown data. Determines sprite mobility: just one sprite, can only turn (gym leaders) or fully mobile.
		public long ptrGraphic; //Pointer to pointer to graphics <- not a typo ;)
		public long LoadCode; //Another unknown pointer.
		Point pntSz;
		//Class vars
		public long trueGraphicsPointer;
		public Collage collage;
		public Bitmap imgBuffer;
		public BloqueImagen rawImage;
		public static Paleta[] myPal;
		private Bitmap[] bi;

		public int mSpriteSize;

		/*			if(renderTileset)
					{
						for(int i = 0; i < 13; i++)
						{
							g.drawImage(globalTiles.getTileSet(i),i*128,0,this);

						}
					}
		*/

		void GrabPal(RomGba rom,int spriteColors)
		{
			OverworldSprites.myPal = new Paleta[16];


			for (int i = 0; i < 16; i++)
			{
				int ptr = new OffsetRom(rom, spriteColors + (i * 8));
				int palNum = rom.Data[spriteColors + (i * 8) + 4] & 0xF;
				OverworldSprites.myPal[palNum] = Paleta.Get( rom.Data.SubArray( ptr, 32));
			}


		}
		void DrawSmall()
		{
			collage = new Collage();
			collage.Add(getTile(0, iPal & 0xf), 0, 0);
			collage.Add(getTile(1, iPal & 0xf), 8, 0);
			collage.Add(getTile(2, iPal & 0xf), 0, 8);
			collage.Add(getTile(3, iPal & 0xf), 8, 8);
		}
		void DrawMedium()
		{
			collage = new Collage();
			collage.Add(getTile(0, iPal & 0xf), 0, 0);
			collage.Add(getTile(1, iPal & 0xf), 8, 0);
			collage.Add(getTile(2, iPal & 0xf), 0, 8);
			collage.Add(getTile(3, iPal & 0xf), 8, 8);
			collage.Add(getTile(4, iPal & 0xf), 0, 16);
			collage.Add(getTile(5, iPal & 0xf), 8, 16);
			collage.Add(getTile(6, iPal & 0xf), 0, 24);
			collage.Add(getTile(7, iPal & 0xf), 8, 24);
		}
		//AutoX and AutoY are only for drawlarge 

		void DrawLarge()
		{
			collage = new Collage();
			try
			{
				collage.Add(getTile(0, iPal & 0xf), 0, 0);
				collage.Add(getTile(1, iPal & 0xf), 8, 0);
				collage.Add(getTile(2, iPal & 0xf), 16, 0);
				collage.Add(getTile(3, iPal & 0xf), 24, 0);
				collage.Add(getTile(4, iPal & 0xf), 0, 8);
				collage.Add(getTile(5, iPal & 0xf), 8, 8);
				collage.Add(getTile(6, iPal & 0xf), 16, 8);
				collage.Add(getTile(7, iPal & 0xf), 24, 8);
				collage.Add(getTile(8, iPal & 0xf), 0, 16);
				collage.Add(getTile(9, iPal & 0xf), 8, 16);
				collage.Add(getTile(10, iPal & 0xf), 16, 16);
				collage.Add(getTile(11, iPal & 0xf), 24, 16);
				collage.Add(getTile(12, iPal & 0xf), 0, 24);
				collage.Add(getTile(13, iPal & 0xf), 8, 24);
				collage.Add(getTile(14, iPal & 0xf), 16, 24);
				collage.Add(getTile(15, iPal & 0xf), 24, 24);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				collage.RemoveAll();
				collage.Add(new Bitmap( 24, 24));
			}

		}
		Bitmap GetImage()
		{
			switch (mSpriteSize)
			{
				case 0:
					DrawSmall();
					break;
				case 1:
					DrawMedium();
					break;
				case 2:
					DrawLarge();
					break;
			}
			return collage.CrearCollage();

		}

		void MakeMeReal(RomGba rom,int sizeSmall,int sizeNormal,int sizeLarge)
		{



			int sz = 0;
			
			if (ptrSize == sizeSmall)
			{
				sz = (4 * 32) / 2;
				mSpriteSize = 0;


			}
			else
			if (ptrSize == sizeNormal)
			{
				sz = (8 * 32) / 2;
				mSpriteSize = 1;


			}
			else
			if (ptrSize == sizeLarge)
			{
				sz = (16 * 32) / 2;
				mSpriteSize = 2;

			}
			else
			{
				sz = (32 * 32) / 2;
				mSpriteSize = 1;


			}

			int offset = (int)trueGraphicsPointer;
			byte[] dBuff = rom.Data.SubArray(offset, sz * 2);
			rawImage = new BloqueImagen() { DatosDescomprimidos = new BloqueBytes(dBuff) };
			rawImage.Paletas.Add(myPal[iPal & 0xF]);//, new Point(128, 128));//pntSz);	

			bi = new Bitmap[16];
			for (int i = 0; i < 16; i++)
			{
				this.bi[i] = rawImage+(OverworldSprites.myPal[i]);
			}
			GetImage();//Honestly not sure what I'm totally doing here. 
		}
		void Load(RomGba rom,int offset,int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{

			StarterWord = new Word(rom, offset);
			offset += Word.LENGTH;
			iPal = rom.Data[offset++];
			sprVald1 = rom.Data[offset++];
			sprVald2 = rom.Data[offset++];
			sprVald3 = rom.Data[offset++];
			FrameSize = new Word(rom, offset);
			offset += Word.LENGTH;
			width = new Word(rom, offset);
			offset += Word.LENGTH;
			height = new Word(rom, offset);
			offset += Word.LENGTH;
			oam1 = new Word(rom, offset);
			offset += Word.LENGTH;
			oam2 = new Word(rom, offset);
			offset += Word.LENGTH;
			ptr2anim = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			ptrSize = new OffsetRom(rom, offset);//Pointer to unknown data. Determines sprite size
			offset += OffsetRom.LENGTH; 
			ptrAnim = new OffsetRom(rom, offset); //Pointer to unknown data. Determines sprite mobility: just one sprite, can only turn (gym leaders) or fully mobile.
			offset += OffsetRom.LENGTH; ;
			ptrGraphic = new OffsetRom(rom, offset); //Pointer to pointer to graphics <- not a typo ;)
			offset += OffsetRom.LENGTH;
			LoadCode = new OffsetRom(rom, offset);
			trueGraphicsPointer = new OffsetRom(rom,(int)ptrGraphic);//Grab the real one
			if (OverworldSprites.myPal == null)
			{
				GrabPal(rom,spriteColors);
			}
			MakeMeReal(rom,sizeSmall,sizeNormal,sizeLarge);
			//if pal size is 0 then we need to grab it
		}
		public Bitmap getTile(int tileNum, int palette)
		{


			int x = tileNum * 8;
			int y = 0;
			Bitmap toSend = new Bitmap(8, 8);
			try
			{
				toSend = bi[palette].Recortar(new Point(x, y),new Size( 8, 8));
			}
			catch (Exception e)
			{
			Console.WriteLine(e.Message);
				//System.out.println("Attempted to read 8x8 at " + x + ", " + y);
			}


			return toSend;
		}

		public OverworldSprites(RomGba rom, int offset,int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{
			Load(rom,offset,spriteColors,sizeSmall,sizeNormal,sizeLarge);
		}

	}
}
