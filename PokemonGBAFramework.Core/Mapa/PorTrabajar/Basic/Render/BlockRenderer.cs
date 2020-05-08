using Gabriel.Cat.S.Drawing;
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

        private Tileset global;
        private Tileset local;
        public static int currentTime = 0;
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

        public Bitmap renderBlock(RomGba rom, int blockNum, int mainTSSize, int mainTsBlcks, int mainTsPalCount, int engine = 1)
        {
            return renderBlock(rom, blockNum, true, mainTSSize, mainTsBlcks,mainTsPalCount, engine);
        }

        public Bitmap renderBlock(RomGba rom, int blockNum, bool transparency, int mainTSSize, int mainTsBlcks,int mainTsPalCount, int engine = 1)
        {
            int origBlockNum = blockNum;
            bool isSecondaryBlock = false;
            if (blockNum >= mainTsBlcks)
            {
                isSecondaryBlock = true;
                blockNum -= mainTsBlcks;
            }

            int blockPointer = (int)((isSecondaryBlock ? local.getTilesetHeader().PBlocks : global.getTilesetHeader().PBlocks) + (blockNum * 16));
            Bitmap block = new Bitmap(16, 16);
            Collage collage = new Collage();
            //Graphics2D g = (Graphics2D)block.getGraphics();
            //g.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_OFF);
            int x = 0;
            int y = 0;
            int layerNumber = 0;

            TripleType type = TripleType.NONE;
            if ((getBehaviorByte(rom, origBlockNum, mainTsBlcks, engine) >> (engine == 1 ? 24 : 8) & 0x30) == 0x30)
                type = TripleType.LEGACY;

            if ((getBehaviorByte(rom, origBlockNum, mainTsBlcks, engine) >> (engine == 1 ? 24 : 8) & 0x40) == 0x40)
            {
                blockPointer += 8;
                type = TripleType.LEGACY2;
            }

            else if ((getBehaviorByte(rom, origBlockNum, mainTsBlcks, engine) >> (engine == 1 ? 24 : 8) & 0x60) == 0x60 && engine == 1)
                type = TripleType.REFERENCE;

            if (type != TripleType.NONE && System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine("Rendering triple tile! " + type.ToString());

            for (int i = 0; i < (type != TripleType.NONE ? 24 : 16); i++)
            {
                if (type == TripleType.REFERENCE && i == 16)
                {
                    bool second = false;
                    int tripNum = (int)((getBehaviorByte(rom, origBlockNum, mainTsBlcks, engine) >> 14) & 0x3FF);
                    if (tripNum >= mainTsBlcks)
                    {
                        second = true;
                        tripNum -= mainTsBlcks;
                    }

                    blockPointer = (int)((second ? local.getTilesetHeader().PBlocks : global.getTilesetHeader().PBlocks) + (tripNum * 16)) + 8;
                    blockPointer -= i;
                }
                int orig = new Word(rom, blockPointer + i);
                int tileNum = orig & 0x3FF;
                int palette = (orig & 0xF000) >> 12;
                bool xFlip = (orig & 0x400) > 0;
                bool yFlip = (orig & 0x800) > 0;
                if (transparency && layerNumber == 0)
                {
                    //try
                    //{
                    //    g.setColor(global.getPalette(currentTime)[palette][0]);
                    //}
                    //catch (Exception e)
                    //{

                    //}
                   collage.Add(new Bitmap(8, 8),x * 8, y * 8);
                }

                if (tileNum < mainTSSize)
                {
                   collage.Add(global.getTile(tileNum, palette, xFlip, yFlip, currentTime,mainTsPalCount), x * 8, y * 8);
                }
                else
                {
                    collage.Add(local.getTile(tileNum - mainTSSize, palette, xFlip, yFlip, currentTime,mainTsPalCount), x * 8, y * 8);
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

        public Block getBlock(RomGba rom,BlockRenderer renderer, int blockNum, int mainTsBlocks, int engine = 1)
        {
            bool isSecondaryBlock = false;
            int realBlockNum = blockNum;
            if (blockNum >= mainTsBlocks)
            {
                isSecondaryBlock = true;
                blockNum -= mainTsBlocks;
            }

            int blockPointer = (int)((isSecondaryBlock ? local.getTilesetHeader().PBlocks : global.getTilesetHeader().PBlocks) + (blockNum * 16));
            int x = 0;
            int y = 0;
            int layerNumber = 0;
            Block b = new Block(rom,renderer,realBlockNum,mainTsBlocks,engine );

            bool tripleTile = false;

            if ((b.backgroundMetaData >> (engine == 1 ? 24 : 8) & 0x30) == 0x30)
            {
                tripleTile = true;
                if (System.Diagnostics.Debugger.IsAttached)
                    Console.WriteLine("Rendering triple tile block!");
            }
            else if ((b.backgroundMetaData >> (engine == 1 ? 24 : 8) & 0x40) == 0x40)
            {

                tripleTile = true;
                blockPointer += 8;
                if (System.Diagnostics.Debugger.IsAttached)
                    Console.WriteLine("Rendering space-saver triple tile block!");
            }

            for (int i = 0; i < (tripleTile ? 24 : 16); i++)
            {
                int orig = new Word(rom, blockPointer + i);
                int tileNum = orig & 0x3FF;
                int palette = (orig & 0xF000) >> 12;
                bool xFlip = (orig & 0x400) > 0;
                bool yFlip = (orig & 0x800) > 0;

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

        public long getBehaviorByte(RomGba rom, int blockID, int mainTsBlocks, int engineVersion = 1)
        {
            int pBehavior = (int)getGlobalTileset().tilesetHeader.PBehavior;
            int blockNum = blockID;
            int offset;
            if (blockNum >= mainTsBlocks)
            {
                blockNum -= mainTsBlocks;
                pBehavior = (int)getLocalTileset().tilesetHeader.PBehavior;
            }
            offset = pBehavior + (blockNum * (engineVersion == 1 ? 4 : 2));
            long bytes = engineVersion == 1 ? new OffsetRom(rom, offset) : new OffsetRom(rom, offset) & 0xFFFF;
            return bytes;
        }
    }
}
