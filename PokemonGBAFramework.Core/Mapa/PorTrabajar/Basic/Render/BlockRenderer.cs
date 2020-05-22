using Gabriel.Cat.S.Drawing;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic.Render
{
    public class BlockRenderer
    {
        public enum TripleType
        {
            NONE,
            LEGACY,
            LEGACY2,
            REFERENCE
        }

        public static int currentTime = 0;

        private Tileset global;
        private Tileset local;

        public BlockRenderer(Tileset global = default, Tileset local = default)
        {
            this.global = global;
            this.local = local;
        }



        public void setGlobalTileset(Tileset global)
        {
            this.global = global;
        }

        public void setLocalTileset(Tileset local)
        {
            this.local = local;
        }


        public Tileset getGlobalTileset()
        {
            return global;
        }

        public Tileset getLocalTileset()
        {
            return local;
        }

        public Bitmap renderBlock(RomGba rom, int blockNum)
        {
            return renderBlock(rom, blockNum, true);
        }

        public Bitmap renderBlock(RomGba rom, int blockNum, bool transparency)
        {
            int orig;
            int tileNum;
            int palette;
            bool xFlip;
            bool yFlip;
            int blockPointer;
            Bitmap block;
            Collage collage;
            TripleType type;
            bool second;
            int tripNum;

            int x = 0;
            int y = 0;
            int layerNumber = 0;
            int origBlockNum = blockNum;
            bool isSecondaryBlock = false;
            int mainTSSize = TilesetHeader.GetMainSize(rom);
            int mainTsBlcks = TilesetHeader.GetMainBlocks(rom);
            DWord behavioByte = getBehaviorByte(rom, origBlockNum);
            if (blockNum >= TilesetHeader.GetMainHeight(rom))
            {
                isSecondaryBlock = true;
                blockNum -= mainTsBlcks;
            }

            blockPointer = (int)((isSecondaryBlock ? local.TilesetHeader.PBlocks : global.TilesetHeader.PBlocks) + (blockNum * 16));
            block = new Bitmap(16, 16);
            collage = new Collage();
             type = TripleType.NONE;
            if ((behavioByte >> (!rom.Edicion.EsRubiOZafiro ? 24 : 8) & 0x30) == 0x30)
                type = TripleType.LEGACY;

            if ((behavioByte >> (!rom.Edicion.EsRubiOZafiro ? 24 : 8) & 0x40) == 0x40)
            {
                blockPointer += 8;
                type = TripleType.LEGACY2;
            }

            else if ((behavioByte >> (!rom.Edicion.EsRubiOZafiro ? 24 : 8) & 0x60) == 0x60 && !rom.Edicion.EsRubiOZafiro)
                type = TripleType.REFERENCE;

            if (type != TripleType.NONE && System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine("Rendering triple tile! " + type.ToString());

            for (int i = 0; i < (type != TripleType.NONE ? 24 : 16); i++)
            {
                if (type == TripleType.REFERENCE && i == 16)
                {
                    second = false;
                    tripNum = (int)((behavioByte >> 14) & 0x3FF);
                    if (tripNum >= mainTsBlcks)
                    {
                        second = true;
                        tripNum -= mainTsBlcks;
                    }

                    blockPointer = (int)((second ? local.TilesetHeader.PBlocks : global.TilesetHeader.PBlocks) + (tripNum * 16)) + 8;
                    blockPointer -= i;
                }
                 orig = new Word(rom,blockPointer + i);
                 tileNum = orig & 0x3FF;
                 palette = (orig & 0xF000) >> 12;
                 xFlip = (orig & 0x400) > 0;
                 yFlip = (orig & 0x800) > 0;
                if (transparency && layerNumber == 0)
                {
                    //try
                    //{
                    //    g.setColor(global.getPalette(currentTime)[palette][0]);
                    //}
                    //catch (Exception e)
                    //{

                    //}
                   collage.Add(new Bitmap(Tile.LADO, Tile.LADO),x * Tile.LADO, y * Tile.LADO);
                }

                if (tileNum < mainTSSize)
                {
                   collage.Add(global.Get(tileNum,palette).Flip(xFlip, yFlip), x * Tile.LADO, y * Tile.LADO);
                }
                else
                {
                    collage.Add(local.Get(tileNum - mainTSSize,palette).Flip(xFlip, yFlip), x * Tile.LADO, y * Tile.LADO);
                }
                x++;
                if (x > 1)
                {
                    x = 0;
                    y++;
                }
                if (y > 1)
                {
                    x = 0;
                    y = 0;
                    layerNumber++;
                }
                i++;
            }
            collage.Base = new ImageBase(block);
            return collage.CrearCollage();
        }

        public Block getBlock(RomGba rom,BlockRenderer renderer, int blockNum)
        {
            int tileNum;
            int palette;
            bool xFlip;
            bool yFlip;
            Block b;
            int orig;
            bool tripleTile;
            int blockPointer;
            bool isSecondaryBlock = false;
            int realBlockNum = blockNum;
            int mainTsBlocks = TilesetHeader.GetMainBlocks(rom);
            int x = 0;
            int y = 0;
            int layerNumber = 0;

            if (blockNum >= mainTsBlocks)
            {
                isSecondaryBlock = true;
                blockNum -= mainTsBlocks;
            }

             blockPointer = (int)((isSecondaryBlock ? local.TilesetHeader.PBlocks : global.TilesetHeader.PBlocks) + (blockNum * 16));
    
          
            b = Block.Get(rom,renderer,realBlockNum);

            tripleTile = false;

            if ((b.backgroundMetaData >> (!rom.Edicion.EsRubiOZafiro ? 24 : 8) & 0x30) == 0x30)
            {
                tripleTile = true;
                if (System.Diagnostics.Debugger.IsAttached)
                    Console.WriteLine("Rendering triple tile block!");
            }
            else if ((b.backgroundMetaData >> (!rom.Edicion.EsRubiOZafiro ? 24 : 8) & 0x40) == 0x40)
            {

                tripleTile = true;
                blockPointer += 8;
                if (System.Diagnostics.Debugger.IsAttached)
                    Console.WriteLine("Rendering space-saver triple tile block!");
            }

            for (int i = 0; i < (tripleTile ? 24 : 16); i++)
            {
                orig = new Word(rom, blockPointer + i);
                 tileNum = orig & 0x3FF;
                 palette = (orig & 0xF000) >> 12;
                 xFlip = (orig & 0x400) > 0;
                 yFlip = (orig & 0x800) > 0;

                //			if(i < 16)
                b.setTile(x + (layerNumber * 2), y, new Tile(tileNum, palette, xFlip, yFlip));
                x++;
                if (x > 1)
                {
                    x = 0;
                    y++;
                }
                if (y > 1)
                {
                    x = 0;
                    y = 0;
                    layerNumber++;
                }
                i++;
            }
            return b;
        }

        public DWord getBehaviorByte(RomGba rom, int blockID)
        {   
            int offset;
            DWord bytes;
            int mainTsBlocks = TilesetHeader.GetMainBlocks(rom);
            int pBehavior = getGlobalTileset().TilesetHeader.PBehavior;
            int blockNum = blockID;

            if (blockNum >= mainTsBlocks)
            {
                blockNum -= mainTsBlocks;
                pBehavior = getLocalTileset().TilesetHeader.PBehavior;
            }
            offset = pBehavior + (blockNum * (rom.Edicion.EsRubiOZafiro ? 2 : 4));
            bytes = new DWord(rom, offset);
            if(rom.Edicion.EsRubiOZafiro)
              bytes&= 0xFFFF;
            return bytes;
        }
    }
}
