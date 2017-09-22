/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/09/2017
 * Hora: 23:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Extension.
	/// </summary>
	public static class Extension
	{
		/// <summary>
		/// Bytes que ocupa un color Convertido con el método de extensión ToGbaBitmap
		/// </summary>
		public const int BYTESPORCOLOR=4;
		//BGRA
		public const int A=3;
		public const int R = A-1;
		public const int G = R - 1;
		public const int B = G - 1;
		
		public static Color[] GetPaleta(this Bitmap bmp)
		{
			int[] paletaInt=GetPaletaInt(bmp);
			Color[] paleta=new Color[paletaInt.Length];
			for(int i=0;i<paletaInt.Length;i++)
				paleta[i]=Color.FromArgb(paletaInt[i]);
			
			return paleta;
			
		}
		public static int[] GetPaletaInt(this Bitmap bmp)
		{
			const int ARGB=4;
			LlistaOrdenada<int,int> dicColors=new LlistaOrdenada<int, int>();
			int pos=0;
			int aux;
			BitmapData bmpData=bmp.LockBits( new Rectangle( 0 , 0 , bmp.Width , bmp.Height ) , ImageLockMode.ReadWrite , PixelFormat.Format32bppArgb );
			unsafe{
				
				int* ptrColorBmp;
				
				ptrColorBmp=(int*)bmpData.Scan0;
				
				for(int i=0,f=bmp.Width*bmp.Height;i<f;i++)
				{
					aux=*ptrColorBmp;
					ptrColorBmp++;
					if(!dicColors.ContainsKey(aux))
						dicColors.Add(aux,pos++);
				}
				
				
			}
			bmp.UnlockBits(bmpData);
			return (int[])dicColors.Keys;
		}
		
		/// <summary>
		/// Lo estandariza para poder trabajar de forma homogenia,los colores son los que se verian en la GBA
		/// </summary>
		/// <param name="bmp"></param>
		/// <returns></returns>
		public static byte[] GetBytes(this Bitmap bmp)
		{
			byte[] bytesImg;
			BitmapData bmpData=bmp.LockBits( new Rectangle( 0 , 0 , bmp.Width , bmp.Height ) , ImageLockMode.ReadWrite , PixelFormat.Format32bppArgb );
			unsafe{
				byte* ptrBytesImg=(byte*)bmpData.Scan0;
				byte* ptrBytes;
				bytesImg=new byte[bmp.Width*bmp.Height*BYTESPORCOLOR];
				fixed(byte* ptBytes=bytesImg)
				{
					ptrBytes=ptBytes;
					for(int i=0;i<bytesImg.Length;i++)
					{
						*ptrBytes=*ptrBytesImg;
						ptrBytes++;
						ptrBytesImg++;
					}
					
					ToGbaColor(ptBytes,bytesImg.Length);
				}
			}
			bmp.UnlockBits(bmpData);
			return bytesImg;
		}
		public static Bitmap ToGbaBitmap(this Bitmap bmp)
		{
			Bitmap bmpNew=new Bitmap(bmp.Width,bmp.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			bmpNew.SetBytes(bmp.GetBytes());
			return bmpNew;
		}

		static unsafe void ToGbaColor(byte* ptrBytesBmp,int total)
		{
			const byte SINTRANSPARENCIA=0xFF;
			const int ARGB=4;
			Color aux;
			
			for (int i = 0; i < total; i+=ARGB)
			{
				aux=Paleta.ToGBAColor(*(ptrBytesBmp+R),*(ptrBytesBmp+G),*(ptrBytesBmp+B));
				
				*(ptrBytesBmp+A)=SINTRANSPARENCIA;
				*(ptrBytesBmp+R)=aux.R;
				*(ptrBytesBmp+G)=aux.G;
				*(ptrBytesBmp+B)=aux.B;
				
				ptrBytesBmp+=ARGB;
			}

			
		}
	}
	
}
