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

        public BlockRenderer(Tileset global = default, Tileset local = default)
        {
            GlobalTileset = global;
            LocalTileset = local;
        }

        public Tileset GlobalTileset { get; set; }
        public Tileset LocalTileset { get; set; }

        public Collage Render(RomGba rom, int blockNum, bool transparency=true)
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
            int mainTSSize = TilesetHeader.GetMainSize(rom);
            int mainTsBlcks = TilesetHeader.GetMainBlocks(rom);
            DWord behavioByte = GetBehaviorByte(rom, origBlockNum);
            bool isSecondaryBlock = blockNum >= mainTsBlcks;
            
            if (isSecondaryBlock)
            {
                blockNum -= mainTsBlcks;
            }

            blockPointer = (int)((isSecondaryBlock ? LocalTileset.TilesetHeader.PBlocks : GlobalTileset.TilesetHeader.PBlocks) + (blockNum * 16));
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

            else if (!rom.Edicion.EsRubiOZafiro && (behavioByte >> 24  & 0x60) == 0x60 )
                type = TripleType.REFERENCE;

            if (type != TripleType.NONE && System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine("Rendering triple tile! " + type.ToString());

            for (int i = 0,iF= (type != TripleType.NONE ? 24 : 16); i < iF; i+=2)
            {
                if (type == TripleType.REFERENCE && i == 16)
                {
                    
                    tripNum = (int)((behavioByte >> 14) & 0x3FF);
                    second = tripNum >= mainTsBlcks;
                    if (second)
                    {
                        tripNum -= mainTsBlcks;
                    }

                    blockPointer = (int)((second ? LocalTileset.TilesetHeader.PBlocks : GlobalTileset.TilesetHeader.PBlocks) + (tripNum * 16)) + 8;
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
                   collage.Add(new Bitmap(Tile.LADO, Tile.LADO).SetColor(GlobalTileset.Paletas[palette][0]),x * Tile.LADO, y * Tile.LADO);
                }

                if (tileNum < mainTSSize)
                {
                   collage.Add(GlobalTileset.Get(tileNum,palette).Flip(xFlip, yFlip), x * Tile.LADO, y * Tile.LADO);
                }
                else
                {
                    collage.Add(LocalTileset.Get(tileNum - mainTSSize,palette).Flip(xFlip, yFlip), x * Tile.LADO, y * Tile.LADO);
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
          
            }
            collage.Base = new ImageBase(block);
            return collage;
        }
        public Bitmap RenderBmp(RomGba rom, int blockNum, bool transparency = true) => Render(rom, blockNum, transparency).CrearCollage();
        public Block GetBlock(RomGba rom,BlockRenderer renderer, int blockNum)
        {
            int tileNum;
            int palette;
            bool xFlip;
            bool yFlip;
            Block bloqueRenderizado;
            int orig;
            bool tripleTile;
            int blockPointer;
            
            int realBlockNum = blockNum;
            int mainTsBlocks = TilesetHeader.GetMainBlocks(rom);
            int x = 0;
            int y = 0;
            int layerNumber = 0;
            bool isSecondaryBlock = blockNum >= mainTsBlocks;
            if (isSecondaryBlock)
            {
                blockNum -= mainTsBlocks;
            }

             blockPointer = (int)((isSecondaryBlock ? LocalTileset.TilesetHeader.PBlocks : GlobalTileset.TilesetHeader.PBlocks) + (blockNum * 16));
    
          
            bloqueRenderizado = Block.Get(rom,renderer,realBlockNum);

            tripleTile = false;

            if ((bloqueRenderizado.BackgroundMetaData >> (!rom.Edicion.EsRubiOZafiro ? 24 : 8) & 0x30) == 0x30)
            {
                tripleTile = true;
                if (System.Diagnostics.Debugger.IsAttached)
                    Console.WriteLine("Rendering triple tile block!");
            }
            else if ((bloqueRenderizado.BackgroundMetaData >> (!rom.Edicion.EsRubiOZafiro ? 24 : 8) & 0x40) == 0x40)
            {

                tripleTile = true;
                blockPointer += 8;
                if (System.Diagnostics.Debugger.IsAttached)
                    Console.WriteLine("Rendering space-saver triple tile block!");
            }

            for (int i = 0,iF= (tripleTile ? 24 : 16); i < iF; i+=2)
            {
                orig = new Word(rom, blockPointer + i);
                 tileNum = orig & 0x3FF;
                 palette = (orig & 0xF000) >> 12;
                 xFlip = (orig & 0x400) > 0;
                 yFlip = (orig & 0x800) > 0;

                //			if(i < 16)
                bloqueRenderizado.SetTile(x + (layerNumber * 2), y, new Tile(tileNum, palette, xFlip, yFlip));
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
            }
            return bloqueRenderizado;
        }

        public DWord GetBehaviorByte(RomGba rom, int blockID)
        {   //quizás los tilesets son otros...es un poco confuso el codigo en java...
            int offset;
            DWord bytes;
            int mainTsBlocks = TilesetHeader.GetMainBlocks(rom);
            int pBehavior = GlobalTileset.TilesetHeader.PBehavior;
            int blockNum = blockID;

            if (blockNum >= mainTsBlocks)
            {
                blockNum -= mainTsBlocks;
                pBehavior = LocalTileset.TilesetHeader.PBehavior;
            }
            offset = pBehavior + (blockNum * (rom.Edicion.EsRubiOZafiro ? 2 : 4));
            bytes = new DWord(rom, offset);
            if(rom.Edicion.EsRubiOZafiro)
              bytes&= 0xFFFF;
            return bytes;
        }
    }
}
