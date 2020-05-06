using System;
using System.Collections.Generic;
using System.Drawing;

//creditos NSE Author por el codigo fuente :)
namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of BloqueSprite.
	/// </summary>
	public class BloqueSprite 
	{
		public enum Medidas
		{
			//creo que no se pueden poner otras por eso hago una enumeracion
			Mini = 8,
			Normal = 16,
			Grande = 32,
			MuyGrande = 64
		}

		public BloqueSprite()
		{
		}
		public BloqueSprite(Bitmap bmp)
		{
			SetBitmapData(bmp);
		}

        public byte[] ImgData { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Offset { get; set; }

        public Paleta Paleta { get; set; }

		public void SetBitmapData(Bitmap bmp, Paleta paleta = null)
		{
			int heghtF = -1, widthF = -1;
			Medidas[] medidas = (Medidas[])Enum.GetValues(typeof(Medidas));

			//pongo las medidas standar
			for (int i = bmp.Height / 8; i < medidas.Length && heghtF < 0; i++)
				if ((int)medidas[i] >= bmp.Height)
					heghtF = (int)medidas[i];
			for (int i = bmp.Width / 8; i < medidas.Length && widthF < 0; i++)
				if ((int)medidas[i] >= bmp.Width)
					widthF = (int)medidas[i];

			if (heghtF < 0)
				heghtF = (int)Medidas.MuyGrande;
			if (widthF < 0)
				widthF = (int)Medidas.MuyGrande;

			bmp = bmp.Clone(new Rectangle(0, 0, widthF, heghtF), System.Drawing.Imaging.PixelFormat.Format32bppArgb);//, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);

			Height = bmp.Height;
			Width = bmp.Width;
			//falta hacer que vaya bien
			ImgData = BloqueImagen.GetDatosDescomprimidos(bmp, paleta);//mirar si va asi
		}


		public Bitmap GetBitmap(Paleta paleta = null)
		{
			if (paleta == null)
				paleta = this.Paleta;
			return BloqueImagen.BuildBitmap(ImgData, paleta, Width, Height);//funciona bien :D
		}
		public static BloqueSprite Get(RomGba rom, Paleta paleta, int offsetBloqueData, int width, int height)
		{
			const int PIXELSPERBYTE = 2;
			BloqueSprite bl = new BloqueSprite();
			bl.Paleta = paleta;
			bl.Width = width;
			bl.Height = height;
			bl.ImgData = rom.Data.SubArray(offsetBloqueData, width * height / PIXELSPERBYTE);
			bl.Offset = offsetBloqueData;
			return bl;
		}
	}
}