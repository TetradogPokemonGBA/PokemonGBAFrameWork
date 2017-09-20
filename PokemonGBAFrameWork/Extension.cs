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

namespace PokemonGBAFrameWork.Extension
{
	/// <summary>
	/// Description of Extension.
	/// </summary>
	public static class Extension
	{
		
		public static Bitmap ChangeFormat(this Bitmap bmp, PixelFormat newFormat)
		{//sacado de tilemap-creator
			/* better?
            using (var temp = new Bitmap(bmp))
            {
                return temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), PixelFormat.Format24bppRgb);
            }*/

			// seems that Bitmap.Clone() does not work correctly?
			Bitmap clone = new Bitmap(bmp.Width, bmp.Height, newFormat);
			using (var g = Graphics.FromImage(clone))
				g.DrawImage(bmp, new Rectangle(0, 0, clone.Width, clone.Height));

			return clone;
		}
		public static Color[] GetPaleta(this Bitmap bmp)
		{//de momento no va del todo bien...
			const int BYTESPERCOLOR=4;
			const int A=0,R=1,G=2,B=3;
			const int ARGB=4;
			LlistaOrdenada<int,int> dicColors=new LlistaOrdenada<int, int>();
			int pos=0;
			int aux;
			int[] intPaleta;
			Color[] paleta;
			PixelFormat pixelFormat=PixelFormat.Format32bppArgb;
			if((bmp.PixelFormat&pixelFormat)!=pixelFormat)
				bmp=bmp.ChangeFormat(pixelFormat);
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
	}
}
