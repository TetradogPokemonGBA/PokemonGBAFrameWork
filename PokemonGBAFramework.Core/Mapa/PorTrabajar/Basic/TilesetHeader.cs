using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class TilesetHeader
    {
        const byte IsPrimaryByte = 0x0;
        const byte IsNotPrimaryByte = 0x1;
        const byte IsCompressedByte = 0x1;
        public  static readonly byte[] HeaderFix = new byte[] { 10, 80, 9, 00, 32, 00, 00 };
        public bool IsCompressed { get; set; }
        public bool IsPrimary { get; set; }
        public byte B2 { get; set; }
        public byte B3 { get; set; }
        public OffsetRom OffsetImagen { get; set; }
        public OffsetRom OffsetPaletas { get; set; }
        public OffsetRom PBlocks { get; set; }
        public OffsetRom PBehavior { get; set; }
        public OffsetRom PAnimation { get; set; }

        public static TilesetHeader Get(RomGba rom, int offset)
        {
            TilesetHeader tilesetHeader = new TilesetHeader();
            tilesetHeader.IsCompressed = rom.Data[offset++]==IsCompressedByte;
            tilesetHeader.IsPrimary = (rom.Data[offset++] == IsPrimaryByte);
            tilesetHeader.B2 = rom.Data[offset++];
            tilesetHeader.B3 = rom.Data[offset++];

            tilesetHeader.OffsetImagen = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            tilesetHeader.OffsetPaletas = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            tilesetHeader.PBlocks = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;

            tilesetHeader.PAnimation = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            tilesetHeader.PBehavior = new OffsetRom(rom, offset);
            return tilesetHeader;
        } 

    }
}
