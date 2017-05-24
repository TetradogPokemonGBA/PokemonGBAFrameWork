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

		public void SetBitmapData(Bitmap bmp)
		{
			int heghtF = -1, widthF = -1;
			Medidas[] medidas = (Medidas[])Enum.GetValues(typeof(Medidas));
			
			//miro que tenga 16 colores diferentes
			
			//pongo las medidas standar
			
			for (int i = 0; i < medidas.Length && heghtF < 0; i++)
				if ((int)medidas[i] >= bmp.Height)
					heghtF = (int)medidas[i];
			for (int i = 0; i < medidas.Length && widthF < 0; i++)
				if ((int)medidas[i] >= bmp.Width)
					widthF = (int)medidas[i];
			
			if (heghtF < 0)
				heghtF = (int)Medidas.MuyGrande;
			if (widthF < 0)
				widthF = (int)Medidas.MuyGrande;
			//hago que tenga el formato correcto para la conversion...falta mirar que sea el correcto
			bmp = bmp.Clone(new Rectangle(0, 0, widthF, heghtF), System.Drawing.Imaging.PixelFormat.Format32bppArgb);//mirar que esten indexados...
			
			height = bmp.Height;
			width = bmp.Width;
			//falta hacer que vaya bien
			imgData=BloqueImagen.GetDatosDescomprimidos(bmp).Bytes;//mirar si va asi
		}

		public bool check(Paleta paleta)
		{//para el testing
			byte[] bytes=imgData;
			bool isOk=true;
			SetBitmapData(GetBitmap(paleta));
			for(int i=0;i<bytes.Length&&isOk;i++)
				isOk=imgData[i].Equals(bytes[i]);
			return isOk;
		}
		public Bitmap GetBitmap(Paleta paleta)
		{
			return BloqueImagen.BuildBitmap(imgData,paleta,width,height);//funciona bien :D
		}
		public static BloqueSprite GetSprite(RomGba rom, int offsetBloqueData, int width, int height)
		{
			const int BYTESCOLOR = 2;
			BloqueSprite bl = new BloqueSprite();
			bl.width = width;
			bl.height = height;
			bl.imgData = rom.Data.SubArray(offsetBloqueData, width * height * BYTESCOLOR);
			return bl;
		}
	}
}
