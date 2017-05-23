/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 23/05/2017
 * Hora: 1:13
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using Gabriel.Cat.Extension;
//creditos NSE Author por el codigo fuente :)
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of BloqueSprite.
	/// </summary>
	public class BloqueSprite
	{
		int width;
		int height;
		byte[] imgData;
		public BloqueSprite()
		{
		}

		public Bitmap GetBitmap(Paleta paleta)
		{
			const byte VISIBLE=0xFF;
			Bitmap bmp=new Bitmap(width,height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			int right,left;
			int pos;
			unsafe{
				bmp.TrataBytes((MetodoTratarBytePointer)((ptr) => {
				                                         	
				                                         	//hago copy and paste
				                                         	for (int y = 0; y < height; y++)
				                                         	{
				                                         		for (int x = 0; x < width; x+=2)
				                                         		{
				                                         			pos = (y - (y % 8)) / 2 * width;
				                                         			pos += (x - (x % 8)) * 4;
				                                         			pos += (y % 8) * 4;
				                                         			pos += (x % 8) / 2;
				                                         			
				                                         			if (pos < imgData.Length)
				                                         			{
				                                         				
				                                         				
				                                         				right = imgData[pos] & 0x0F;
				                                         				left = imgData[pos] >> 4;

				                                         				if (left != 0)
				                                         				{
				                                         					*(ptr + 4 * (y * width + x + 1)) = paleta[left].B;
				                                         					*(ptr + 4 * (y * width + x + 1) + 1) = paleta[left].G;
				                                         					*(ptr + 4 * (y * width + x + 1) + 2) = paleta[left].R;
				                                         					*(ptr + 4 * (y * width + x + 1) + 3) = VISIBLE;
				                                         				}
				                                         			  
				                                         				if (right != 0)
				                                         				{
				                                         					*(ptr + 4 * (y * width + x)) = paleta[right].B;
				                                         					*(ptr + 4 * (y * width + x) + 1) = paleta[right].G;
				                                         					*(ptr + 4 * (y * width + x) + 2) = paleta[right].R;
				                                         					*(ptr + 4 * (y * width + x) + 3) = VISIBLE;
				                                         				}
				                                         				

				                                         				
				                                         				
				                                         				
				                                         			}
				                                         		}
				                                         	}
				                                         	
				                                         	
				                                         }));
				
			}
			
			return bmp;
		}
		public static BloqueSprite GetSprite(RomGba rom, int offsetBloqueData,int width,int height)
		{
			const int BYTESCOLOR=2;
			BloqueSprite bl=new BloqueSprite();
			bl.width=width;
			bl.height=height;
			bl.imgData=rom.Data.SubArray(offsetBloqueData,width*height*BYTESCOLOR);
			return bl;
		}
	}
}
