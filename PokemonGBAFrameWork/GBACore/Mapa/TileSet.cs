using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork.Mapa
{
    public class TileSet
    {
        public const int PRIMARYSIZE = 0x200;
        public const int TOTALPALETAS = 12;

        public Llista<MetaTile> MetaTiles { get; set; }
        public Paleta[] Paletas { get; private set; }
        //de momento lo pongo asi, cuando sepa como va lo pongo entendible
        string name;
        string isCompresed;
        string isSecundary;
        string padding;
        string tilesLabel;
        string palettesLabel;
        string metaTilesLabel;
        string callBackLabel;
        string metaTileAttrsLabel;
        public List<Bitmap> Tiles { get; private set; }

        public TileSet()
        {
            Paletas = new Paleta[TOTALPALETAS];
            MetaTiles = new Llista<MetaTile>();
            Tiles = new List<Bitmap>();
        }
        public static Bitmap ToBitmap(int tileIndex, TileSet primaryTileSet, TileSet secundaryTileSet)
        {//https://github.com/TetradogOther/pretmap/blob/master/tileset.cpp mirar que este bien pasado
            Bitmap bmp = new Bitmap(16, 16);
            MetaTile metaTile = MetaTile.GetMetaTile(tileIndex, primaryTileSet, secundaryTileSet);
            TileSet bloqueTileset;
            Paleta[] paletas;
            Bitmap bmpTile;
            Tile tile;
            Paleta paletaTile;
            Color pixelBmpTile;
            if (metaTile == null || metaTile.Tiles.Count == 0)
            {
                //fill 4 bytes 0xFF en bmp por mirar...
            }
            else
            {
                bloqueTileset = GetTileSet(tileIndex, primaryTileSet, secundaryTileSet);
                if (bloqueTileset == null)
                {
                    //fill 4 bytes 0xFF en bmp por mirar...
                }
                else
                {
                    paletas = GetPaletas(primaryTileSet, secundaryTileSet);
                    for (int layer = 0; layer < 2; layer++)
                        for (int y = 0; y < 2; y++)
                            for (int x = 0; x < 2; x++)
                            {
                                tile = metaTile.Tiles[(y * 2) + x + (layer * 4)];
                                bmpTile = MetaTile.GetMetaTileTile(tile.Index, primaryTileSet, secundaryTileSet);
                                if (bmpTile != null)
                                {
                                    if (paletas.Length > tile.PaletteIndex)
                                    {
                                        paletaTile = paletas[tile.PaletteIndex];
                                        for (int j = 0; j < Paleta.LENGTH; j++)
                                        {
                                            bmpTile.SetPixel(j, 0, paletaTile[j]);//la parte de X,Y no se como va todavia...
                                        }
                                    }
                                    else
                                    {
                                        //la paleta no es correcta...
                                    }
                                    if (layer > 0)
                                    {
                                        pixelBmpTile = bmpTile.GetPixel(15, 0);//la parte de X,Y...
                                        pixelBmpTile = Color.FromArgb(0, pixelBmpTile.R, pixelBmpTile.G, pixelBmpTile.B);
                                        bmpTile.SetPixel(15, 0, pixelBmpTile);//parte X,Y...
                                    }
                                    tile.AplicaFlip(bmpTile);
                                    PonImagen(bmp, bmpTile, new Point(x * 8, y * 8));
                                }
                                //else{
                                // Some metatiles specify tiles that are outside the valid range.
                                // These are treated as completely transparent, so they can be skipped without
                                // being drawn.
                                //}
                            }
                }
            }
            //pongo los pixeles :)

            return bmp;
        }

        private static void PonImagen(Bitmap bmp, Bitmap bmpTile, Point locationBmpTile)
        {//por optimizar
            for (int x = 0; x < bmpTile.Size.Width; x++)
                for (int y = 0; y < bmpTile.Size.Height; y++)
                    bmp.SetPixel(x + locationBmpTile.X, y + locationBmpTile.Y, bmpTile.GetPixel(x, y));
        }



        public static TileSet GetTileSet(int index, TileSet primaryTileSet, TileSet secundaryTileSet)
        {

            return index < PRIMARYSIZE ? primaryTileSet : secundaryTileSet;
        }
        public static int GetIndexBloque(int index)
        {
            return index < PRIMARYSIZE ? index : index - PRIMARYSIZE;
        }
        public static Paleta[] GetPaletas(TileSet primaryTileSet, TileSet secundaryTileSet)
        {
            Paleta[] paletas = new Paleta[TOTALPALETAS];
            for (int i = 0, j = TOTALPALETAS / 2; j < TOTALPALETAS; i++, j++)
            {
                paletas[i] = primaryTileSet.Paletas[i];
                paletas[j] = secundaryTileSet.Paletas[j];
            }
            return paletas;
        }
    }
}
