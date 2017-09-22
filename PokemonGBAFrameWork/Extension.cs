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
		public const int R = 1;
		public const int G = R + 1;
		public const int B = G + 1;
		
		public static Color[] GetPaleta(this Bitmap bmp)
		{
			const int ARGB=4;
			const int A=0;
			LlistaOrdenada<int,int> dicColors=new LlistaOrdenada<int, int>();
			int pos=0;
			int aux;
			int[] intPaleta;
			Color[] paleta;
			PixelFormat pixelFormat=PixelFormat.Format32bppArgb;
			if((bmp.PixelFormat&pixelFormat)!=pixelFormat)
				bmp=bmp.Clone(new Rectangle(0,0,bmp.Width,bmp.Height),pixelFormat);
			unsafe{
				byte* ptrColorBmp;
				fixed(byte* ptrBytesBmp=bmp.GetBytes())
				{
					ptrColorBmp=ptrBytesBmp;
					
					for(int i=0,f=bmp.Width*bmp.Height;i<f;i++)
					{
						aux=Serializar.ToInt(new byte[]{*(ptrColorBmp+A),*(ptrColorBmp+R),*(ptrColorBmp+G),*(ptrColorBmp+B)});
						ptrColorBmp+=ARGB;
						if(!dicColors.ContainsKey(aux))
							dicColors.Add(aux,pos++);
					}
				}
				
			}
			paleta=new Color[dicColors.Count];
			intPaleta=(int[])dicColors.Keys;
			for(int i=0;i<dicColors.Count;i++)
				paleta[i]=Color.FromArgb(intPaleta[i]);
			
			return paleta;
			
		}
		
		/// <summary>
		/// Lo estandariza para poder trabajar de forma homogenia,los colores son los que se verian en la GBA
		/// </summary>
		/// <param name="bmp"></param>
		/// <returns></returns>
		public static byte[] GetBytes(this Bitmap bmp)
		{
			byte[] bytesImg;
			bmp = bmp.Clone(new Rectangle(0,0,bmp.Width,bmp.Height),System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			bytesImg=Gabriel.Cat.Extension.Extension.GetBytes(bmp);
			unsafe{
				fixed(byte* ptrBytesImg=bytesImg)
					ToGbaColor(ptrBytesImg,bytesImg.Length);
			}
			return bytesImg;
		}
		public static Bitmap ToGbaBitmap(this Bitmap bmp)
		{
			bmp.SetBytes(bmp.GetBytes());
			return bmp;
		}

		static unsafe void ToGbaColor(byte* ptrBytesBmp,int total)
		{
			const byte SINTRANSPARENCIA=0xFF;
			const int ARGB=4;
			Color aux;

			for (int i = 0; i < total; i+=ARGB)
			{
				aux=Paleta.ToGBAColor(*(ptrBytesBmp+R),*(ptrBytesBmp+G),*(ptrBytesBmp+B));
				
				*ptrBytesBmp=SINTRANSPARENCIA;
				ptrBytesBmp++;
				
				*ptrBytesBmp=aux.R;
				ptrBytesBmp++;
				
				*ptrBytesBmp=aux.G;
				ptrBytesBmp++;
				
				*ptrBytesBmp=aux.B;
				ptrBytesBmp++;
			}

			
		}
	}
	
}
