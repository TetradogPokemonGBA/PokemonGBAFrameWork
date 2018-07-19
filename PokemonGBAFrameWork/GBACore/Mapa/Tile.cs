using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork.Mapa
{
    public class Tile
    {
        public static readonly Size Size = new Size(8, 8);
        public int Index { get; set; }
        public bool XFlip { get; set; }
        public bool YFlip { get; set; }
        public int PaletteIndex { get; set; }

        public  void AplicaFlip(Bitmap bmpTile)
        {//por optimizar
            Color aux;
            if (XFlip)
            {
                for (int x = 0; x < bmpTile.Size.Width / 2; x++)
                    for (int y = 0; y < bmpTile.Size.Height; y++)
                    {
                        aux = bmpTile.GetPixel(x, y);
                        bmpTile.SetPixel(x, y, bmpTile.GetPixel(bmpTile.Size.Width - x - 1, y));
                        bmpTile.SetPixel(bmpTile.Size.Width - x - 1, y, aux);

                    }
            }
            if (YFlip)
            {
                for (int y = 0; y < bmpTile.Size.Height / 2; y++)
                    for (int x = 0; x < bmpTile.Size.Width; x++)

                    {
                        aux = bmpTile.GetPixel(x, y);
                        bmpTile.SetPixel(x, y, bmpTile.GetPixel(x, bmpTile.Size.Height - y - 1));
                        bmpTile.SetPixel(x, bmpTile.Size.Height - y - 1, aux);

                    }
            }
        }
    }
}
