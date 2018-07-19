using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork.GBACore.Mapa.Mapa
{
    public class PartBlock
    {
        public static readonly Size Size = new Size(8, 8);
        public int TileIndex { get; set; }
        public bool XFlip { get; set; }
        public bool YFlip { get; set; }
        public int PaletteIndex { get; set; }

    }
}
