/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 23/05/2017
 * Hora: 1:13
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
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
		internal enum Medidas
		{
			//creo que no se pueden poner otras por eso hago una enumeracion
			Mini = 8,
			Normal = 16,
			Grande = 32,
			MuyGrande = 64
		}
		
		int offset;
		int width;
		int height;
		byte[] imgData;
		Paleta paleta;
		
		private BloqueSprite()
		{
		}
		public BloqueSprite(Bitmap bmp)
		{
			SetBitmapData(bmp);
		}

		public byte[] ImgData {
			get {
				return imgData;
			}
		}
		public int Height {
			get {
				return height;
			}
		}
		public int Width {
			get {
				return width;
			}
		}
		public int Offset {
			get {
				return offset;
			}
			private set {
				offset = value;
			}
		}

		public Paleta Paleta {
			get {
				return paleta;
			}
		}

		public void SetBitmapData(Bitmap bmp,Paleta paleta=null)
		{
			int heghtF = -1, widthF = -1;
			Medidas[] medidas = (Medidas[])Enum.GetValues(typeof(Medidas));
			
			//pongo las medidas standar
			for (int i = bmp.Height/8; i < medidas.Length && heghtF < 0; i++)
				if ((int)medidas[i] >= bmp.Height)
					heghtF = (int)medidas[i];
			for (int i = bmp.Width/8; i < medidas.Length && widthF < 0; i++)
				if ((int)medidas[i] >= bmp.Width)
					widthF = (int)medidas[i];
			
			if (heghtF < 0)
				heghtF = (int)Medidas.MuyGrande;
			if (widthF < 0)
				widthF = (int)Medidas.MuyGrande;
			
			bmp = bmp.Clone(new Rectangle(0, 0, widthF, heghtF),System.Drawing.Imaging.PixelFormat.Format32bppArgb);//, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
			
			height = bmp.Height;
			width = bmp.Width;
			//falta hacer que vaya bien
			imgData=BloqueImagen.GetDatosDescomprimidos(bmp,paleta);//mirar si va asi
		}


		public Bitmap GetBitmap(Paleta paleta=null)
		{
			if(paleta==null)
				paleta=this.Paleta;
			return BloqueImagen.BuildBitmap(imgData,paleta,width,height);//funciona bien :D
		}
		public static BloqueSprite GetSprite(RomGba rom,Paleta paleta, int offsetBloqueData, int width, int height)
		{
			const int PIXELSPERBYTE = 2;
			BloqueSprite bl = new BloqueSprite();
			bl.paleta=paleta;
			bl.width = width;
			bl.height = height;
			bl.imgData = rom.Data.SubArray(offsetBloqueData, width * height / PIXELSPERBYTE);
			bl.Offset=offsetBloqueData;
			return bl;
		}
	}
}
