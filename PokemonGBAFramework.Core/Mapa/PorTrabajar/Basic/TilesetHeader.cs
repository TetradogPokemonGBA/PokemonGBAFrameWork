using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class TilesetHeader
    {
        public byte BCompressed { get; set; }
        public bool IsPrimary { get; set; }
        public byte B2 { get; set; }
        public byte B3 { get; set; }
        public long PGFX { get; set; }
        public long OffsetPaletas { get; set; }
        public long PBlocks { get; set; }
        public long PBehavior { get; set; }
        public long PAnimation { get; set; }
        public long HdrSize { get; set; }
        public int BOffset { get; set; }
        public RomGba Rom { get; set; }//de momento lo dejo luego ya lo pondré mejor

        public TilesetHeader(RomGba rom, int offset,int engine=1)
        {
            this.Rom = rom;
            BOffset = offset;
            BCompressed = rom.Data[BOffset++];
            IsPrimary = (rom.Data[BOffset++] == 0);//Reflect this when saving
            B2 = rom.Data[BOffset++];
            B3 = rom.Data[BOffset++];

            PGFX = new OffsetRom(rom, BOffset);
            BOffset += OffsetRom.LENGTH;
            OffsetPaletas = new OffsetRom(rom, BOffset);
            BOffset += OffsetRom.LENGTH;
            PBlocks = new OffsetRom(rom, BOffset);
            BOffset += OffsetRom.LENGTH;
            if (engine == 1)
            {
                PAnimation = new OffsetRom(rom, BOffset);
                BOffset += OffsetRom.LENGTH;
                PBehavior = new OffsetRom(rom, BOffset);
                BOffset += OffsetRom.LENGTH;
            }
            else
            {
                PBehavior = new OffsetRom(rom, BOffset);
                BOffset += OffsetRom.LENGTH;
                PAnimation = new OffsetRom(rom, BOffset);
                BOffset += OffsetRom.LENGTH;
            }
            HdrSize = BOffset - offset;

        }


        //public void save()
        //{
        //	rom.Seek(bOffset);
        //	rom.writeByte(bCompressed);
        //	rom.writeByte((byte)(isPrimary ? 0x0 : 0x1));
        //	rom.writeByte(b2);
        //	rom.writeByte(b3);

        //	rom.writePointer((int)pGFX);
        //	rom.writePointer((int)pPalettes);
        //	rom.writePointer((int)pBlocks);
        //	rom.writePointer((int)pAnimation);
        //	rom.writePointer((int)pBehavior);
        //}
    }
}
