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
		enum Medidas
		{//creo que no se pueden poner otras por eso hago una enumeracion
			Mini = 8,
			Normal = 16,
			Grande = 32,
			MuyGrande = 64
		}
		int width;
		int height;
		byte[] imgData;
		private BloqueSprite()
		{
		}
		public BloqueSprite(Bitmap bmp)
		{
			SetBitmapData(bmp);
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


		public Bitmap GetBitmap(Paleta paleta)
		{
			return BloqueImagen.BuildBitmap(imgData,paleta,width,height);//funciona bien :D
		}
		public static BloqueSprite GetSprite(RomGba rom, int offsetBloqueData, int width, int height)
		{
			const int PIXELSPERBYTE = 2;
			BloqueSprite bl = new BloqueSprite();
			bl.width = width;
			bl.height = height;
			bl.imgData = rom.Data.SubArray(offsetBloqueData, width * height / PIXELSPERBYTE);
			return bl;
		}
	}
}
