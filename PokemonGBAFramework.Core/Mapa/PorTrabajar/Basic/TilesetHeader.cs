using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class TilesetHeader
    {
        const int MainHeightRubiYZafiro = 0x100;
        const int LocalHeightRubiYZafiro = 0x100;
        const int MainHeightGeneral = 0x140;
        const int LocalHeightGeneral = 0xC0;

        const int MainSizeRubiYZafiro = 0x200;
        const int MainSizeGeneral = 0x280;

        const int LocalSizeRubiYZafiro = 0x200;
        const int LocalSizeGeneral = 0x140;

        const int MainBlocksKanto=0x280;
        const int MainBlocksHoenn = 0x200;

        const int PaletaCountRubiYZafiro = 6;
        const int PaletaCountKanto = 8;
        const int PaletaCountEsmeralda = 7;

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

        public static TilesetHeader Get(RomGba rom, int offsetTilesetHeader)
        {
            int offset = offsetTilesetHeader;
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

            if (!rom.Edicion.EsRubiOZafiro)
            {
                tilesetHeader.PAnimation = new OffsetRom(rom, offset);
                offset += OffsetRom.LENGTH;
                tilesetHeader.PBehavior = new OffsetRom(rom, offset);
            }
            else
            {
                tilesetHeader.PBehavior = new OffsetRom(rom, offset);
                offset += OffsetRom.LENGTH;
                tilesetHeader.PAnimation = new OffsetRom(rom, offset);
            }

            if (tilesetHeader.OffsetImagen.IsEmpty)
                tilesetHeader.OffsetImagen = default;

            if (tilesetHeader.OffsetPaletas.IsEmpty)
                tilesetHeader.OffsetPaletas = default;

            if (tilesetHeader.PAnimation.IsEmpty)
                tilesetHeader.PAnimation = default;

            if (tilesetHeader.PBlocks.IsEmpty)
                tilesetHeader.PBlocks = default;

            if (tilesetHeader.PBehavior.IsEmpty)
                tilesetHeader.PBehavior = default;

            return tilesetHeader;
        }

        public static int GetLocalHeight(RomGba rom)
        {
            return GetLocalHeight(rom.Edicion);
        }

        public static int GetLocalHeight(Edicion edicion)
        {
            return edicion.EsRubiOZafiro ? LocalHeightRubiYZafiro : LocalHeightGeneral;
        }

        public static int GetMainHeight(RomGba rom)
        {
            return GetMainHeight(rom.Edicion);
        }

        public static int GetMainHeight(Edicion edicion)
        {
            return edicion.EsRubiOZafiro ? MainHeightRubiYZafiro : MainHeightGeneral;
        }
        public static int GetMainBlocks(RomGba rom)
        {
            return GetMainBlocks(rom.Edicion);
        }

        public static int GetMainBlocks(Edicion edicion)
        {
            return edicion.EsKanto ? MainBlocksKanto : MainBlocksHoenn;
        }
        public static int GetPaletaCount(RomGba rom)
        {
            return GetPaletaCount(rom.Edicion);
        }

        public static int GetPaletaCount(Edicion edicion)
        {
            return edicion.EsKanto ? PaletaCountKanto :edicion.EsEsmeralda? PaletaCountEsmeralda:PaletaCountRubiYZafiro;
        }
        public static int GetMainSize(RomGba rom)
        {
            return GetMainSize(rom.Edicion);
        }

        public static int GetMainSize(Edicion edicion)
        {
            return edicion.EsRubiOZafiro ?  MainSizeRubiYZafiro : MainSizeGeneral;
        }
        public static int GetLocalSize(RomGba rom)
        {
            return GetLocalSize(rom.Edicion);
        }

        public static int GetLocalSize(Edicion edicion)
        {
            return edicion.EsRubiOZafiro ? LocalSizeRubiYZafiro : LocalSizeGeneral;
        }
    }
}
