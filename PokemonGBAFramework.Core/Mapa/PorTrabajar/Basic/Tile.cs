using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Tile:ICloneable
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
					indexTile = MaxIndexTile;
				else if (value < 0)
					indexTile = 0;
				else
					indexTile = value;
			}
		}
		public int IndexPaleta
		{
			get => indexPaleta;
			set
			{
				if (value > MaxIndexPaleta)
					indexPaleta = MaxIndexPaleta;
				else if (value < 0)
					indexPaleta = 0;
				else
					indexPaleta = value;
			}
		}

		public bool XFlip { get; set; }
		public bool YFlip { get; set; }

		public Tile Clon()
		{
			return new Tile(IndexTile, IndexPaleta, XFlip, YFlip);
		}

        object ICloneable.Clone()
        {
			return Clon();
        }
    }
}