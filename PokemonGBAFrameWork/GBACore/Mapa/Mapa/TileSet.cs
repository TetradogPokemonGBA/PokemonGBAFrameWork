using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork.GBACore.Mapa.Mapa
{
   public class TileSet
    {
        public const int PRIMARYSIZE = 0x200;
        public const int TOTALPALETAS = 12;

        public Paleta[] Paletas { get; private set; }
        public List<BloqueImagen> Tiles { get; private set; }
        public List<Block> Bloques { get; private set; }

        public static Bitmap ToBitmap(TileSet primary,TileSet secundary,int index)
        {

        }
        public static  BloqueImagen GetTile(int index, TileSet primaryTileSet, TileSet secundaryTileSet)
        {
            TileSet tileSet = TileSet.GetTileSet(index, primaryTileSet, secundaryTileSet);
            int localIndex = TileSet.GetIndexBloque(index);
            BloqueImagen tile;
            if (tileSet != null && tileSet.Tiles.Count >= localIndex)
            {
                tile = tileSet.Tiles[localIndex];
            }
            else
            {
                tile =new BloqueImagen(PartBlock.Size.Height);
            }
            return tile;
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
