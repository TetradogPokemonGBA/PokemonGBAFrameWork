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


        public byte[] ImgData { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Offset { get; set; }

        public Paleta Paleta { get; set; }




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