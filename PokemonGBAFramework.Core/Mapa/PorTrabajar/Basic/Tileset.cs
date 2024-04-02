using Gabriel.Cat.S.Extension;
using PokemonGBAFramework.Core.Extension;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Tileset
    {
        public enum DayLight
        {
            Normal, Morning, Day, Afternoon, Evening, Night, Auto
        }
        public static DayLight Actual { get; set; } = DayLight.Normal;
        public static Color BackgroundColor { get; set; } = Color.FromArgb(24, 48, 80);

        public const int NUMBLOCKS = 1024;
		public const int MAXTIME =4;
		public const int MINTIME = 1;
		public const int MAXFILA = 16;

	

		GranPaleta paletasOriginal;
 

		public TilesetHeader TilesetHeader { get; private set; }

		public bool Modified { get; set; } = false;
		public GranPaleta Paleta =>paletasOriginal;
	


	





		public static Tileset Get(RomGba rom,int offsetTilesetHeader, OffsetRom offsetTilesets= default)
		{
			return Get(rom,TilesetHeader.Get(rom, offsetTilesetHeader,offsetTilesets));
		}
		public static Tileset Get(RomGba rom,TilesetHeader tilesetHeader)
		{

            Tileset tileset = new Tileset
            {
                TilesetHeader = tilesetHeader
            };



            tileset.paletasOriginal = GetPaleta(rom, tileset.TilesetHeader.OffsetPaletas);
           

			return tileset;
		}



		public static GranPaleta GetPaleta(RomGba rom, OffsetRom offsetTablaPaletasTileset)
		{
			return GranPaleta.Get(rom, offsetTablaPaletasTileset);
		}


     
 



    }
}
