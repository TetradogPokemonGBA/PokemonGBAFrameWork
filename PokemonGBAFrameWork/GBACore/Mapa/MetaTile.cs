using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace PokemonGBAFrameWork.Mapa
{
    public class MetaTile
    {
        public Llista<Tile> Tiles { get; private set; }
        public int Attr { get; set; }

        public MetaTile()
        {
            Tiles = new Llista<Tile>();
        }

        public static Bitmap GetMetaTileTile(int index,TileSet primaryTileSet,TileSet secundaryTileSet)
        {
            TileSet tileSet = TileSet.GetTileSet(index, primaryTileSet, secundaryTileSet);
            int localIndex = TileSet.GetIndexBloque(index);
            Bitmap tile;
            if(tileSet!=null&&tileSet.Tiles.Count>=localIndex)
            {
                tile = tileSet.Tiles[localIndex];
            }
            else
            {
                tile = new Bitmap(Tile.Size.Width,Tile.Size.Height);
            }
            return tile;
        }
        public static MetaTile GetMetaTile(int index, TileSet primaryTileSet, TileSet secundaryTileSet)
        {
            TileSet tileSet = TileSet.GetTileSet(index, primaryTileSet, secundaryTileSet);
            int localIndex = TileSet.GetIndexBloque(index);
            MetaTile metaTile = null;
            if (tileSet != null&&tileSet.MetaTiles.Count>=localIndex)
            {
                metaTile = tileSet.MetaTiles[localIndex];
            }

            return metaTile;
        }
    }
}
