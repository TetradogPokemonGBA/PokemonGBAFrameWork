using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork.GBACore.Mapa.Mapa
{
    public class Block
    {
        const int PARTESCAPA = 4;
        const int PARTESBLOQUE = PARTESCAPA * 2;

        PartBlock[,] capaTrasera;
        PartBlock[,] capaDelantera;
        int atributos;
        public Block()
        {
            capaTrasera = new PartBlock[2, 2];
            capaDelantera = new PartBlock[2, 2];
        }
        //se tiene que rehacer un poco...
        public Bitmap ToBitmap(TileSet tileSet)
        {
            return ToBitmap(tileSet.Tiles, tileSet.Paletas);
        }
        public Bitmap ToBitmap(IList<BloqueImagen> tiles, IList<Paleta> paletas)
        {
            Bitmap bmp = new Bitmap(PartBlock.Size.Width * 2, PartBlock.Size.Height * 2);
            Bitmap empty = new Bitmap(PartBlock.Size.Width, PartBlock.Size.Height);
            Bitmap aux;
            List<PartBlock[,]> bloque = new List<PartBlock[,]>();
            bloque.Add(capaTrasera);
            bloque.Add(capaDelantera);
            for (int i = 0; i < bloque.Count; i++)
                for (int x = 0; x < PartBlock.Size.Width; x++)
                    for (int y = 0; y < PartBlock.Size.Height; y++)
                    {
                        if (tiles.Count > bloque[i][x, y].TileIndex && paletas.Count > bloque[i][x, y].PaletteIndex)
                            aux = tiles[bloque[i][x, y].TileIndex] + paletas[bloque[i][x, y].PaletteIndex];
                        else aux = empty;

                        PonImagen(bmp, new Point(x * PartBlock.Size.Width, y * PartBlock.Size.Height), aux, bloque[i][x, y].XFlip, bloque[i][x, y].YFlip);
                    }
            return bmp;

        }

        private void PonImagen(Bitmap bmp, Point locationPart, Bitmap part, bool xFlip, bool yFlip)
        {
            throw new NotImplementedException();
        }
        public static PartBlock[] ToPartBlockArray(IList<Block> blocks)
        {
            PartBlock[] partBlocks = new PartBlock[blocks.Count * PARTESBLOQUE];
            int pos = 0;
            List<PartBlock[,]> bloque = new List<PartBlock[,]>();

            for (int j = 0; j < blocks.Count; j++)
            {
                bloque.Clear();
                bloque.Add(blocks[j].capaTrasera);
                bloque.Add(blocks[j].capaDelantera);
                for (int i = 0; i < bloque.Count; i++)
                    for (int x = 0; x < PartBlock.Size.Width; x++)
                        for (int y = 0; y < PartBlock.Size.Height; y++)
                        {
                            partBlocks[pos++] = bloque[i][x, y];
                        }
            }
            return partBlocks;

        }
        public static Block[] ToBlockArray(IList<PartBlock> partsBlock)
        {

            if (partsBlock.Count % PARTESBLOQUE != 0)
                throw new ArgumentException(string.Format("cada bloque se compone de {0} partes!",PARTESBLOQUE));
            Block[] bloques = new Block[partsBlock.Count / PARTESBLOQUE];
            int pos = 0;
            for (int i = 0; i < bloques.Length; i++)
            {
                for (int x = 0; x < PartBlock.Size.Width; x++)
                    for (int y = 0; y < PartBlock.Size.Height; y++)
                    {
                        bloques[i].capaTrasera[x, y] = partsBlock[pos];
                        bloques[i].capaDelantera[x, y] = partsBlock[pos+PARTESCAPA];
                        pos++;
                    }
            }
            return bloques;
        }

        public static Block GetBlock(int index, TileSet primaryTileSet, TileSet secundaryTileSet)
        {
            TileSet tileSet = TileSet.GetTileSet(index, primaryTileSet, secundaryTileSet);
            int localIndex = TileSet.GetIndexBloque(index);
            Block metaTile = null;
            if (tileSet != null && tileSet.Bloques.Count >= localIndex)
            {
                metaTile = tileSet.Bloques[localIndex];
            }

            return metaTile;
        }
    }
}
