using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Tile
	{
		public const int MaxIndexTile = 0x3FF;
		public const int MaxIndexPaleta = 12;
		public const int LADO = 8;

		private int indexTile;
		private int indexPaleta;

		public Tile(int tileNum, int palette, bool xFlip, bool yFlip)
		{
			IndexTile = tileNum;
			IndexPaleta = palette;
			XFlip = xFlip;
			YFlip = yFlip;
		}


		public int IndexTile
		{
			get => indexTile;
			set
			{
				if (value > MaxIndexTile)
					value = MaxIndexTile;

				indexTile = value;
			}
		}
		public int IndexPaleta
		{
			get => indexPaleta;
			set
			{
				if (value > MaxIndexPaleta)
					value = MaxIndexPaleta;

				indexPaleta = value;
			}
		}

		public bool XFlip { get; set; }
		public bool YFlip { get; set; }

		public Tile getNewInstance()
		{
			return new Tile(indexTile, indexPaleta, XFlip, YFlip);
		}
	}
}