using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Tile
	{
		public const int MaxTileNum = 0x3FF;
		public const int MaxPaletaNum = 12;

		private int tileNum;
		private int pal;

		public Tile(int tileNum, int palette, bool xFlip, bool yFlip)
		{
			TileNumber = tileNum;
			PaletteNum = palette;
			XFlip = xFlip;
			YFlip = yFlip;
		}


		public int TileNumber
		{
			get => tileNum;
			set
			{
				if (value > MaxTileNum)
					value = MaxTileNum;

				tileNum = value;
			}
		}
		public int PaletteNum
		{
			get => pal;
			set
			{
				if (value > MaxPaletaNum)
					value = MaxPaletaNum;

				pal = value;
			}
		}

		public bool XFlip { get; set; }
		public bool YFlip { get; set; }

		public Tile getNewInstance()
		{
			return new Tile(tileNum, pal, XFlip, YFlip);
		}
	}
}