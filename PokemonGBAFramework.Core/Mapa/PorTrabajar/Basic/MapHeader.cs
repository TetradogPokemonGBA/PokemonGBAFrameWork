using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class MapHeader
    {
        public OffsetRom OffsetMap { get; set; }
        public OffsetRom OffsetSprites { get; set; }
        public OffsetRom OffsetScript { get; set; }
        public OffsetRom OffsetConnect { get; set; }
        public Word Song { get; set; }
        public Word Map { get; set; }
        public byte LabelID { get; set; }
        public byte Flash { get; set; }
        public byte Weather { get; set; }
        public byte Type { get; set; }
        public byte SinUso1 { get; set; }
        public byte SinUso2 { get; set; }
        public byte LabelToggle { get; set; }
        public byte SinUso3 { get; set; }

        public static MapHeader Get(RomGba rom, OffsetRom offsetMapHeader)
        {
            MapHeader mapHeader = new MapHeader();
            int offset = offsetMapHeader;// & 0x1FFFFFF;//no se porque hacen eso...

            mapHeader.OffsetMap = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.OffsetSprites = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.OffsetScript = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.OffsetConnect = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.Song = new Word(rom, offset);
            offset += Word.LENGTH;
            mapHeader.Map = new Word(rom, offset);
            offset += Word.LENGTH;

            mapHeader.LabelID = rom.Data[offset++];
            mapHeader.Flash = rom.Data[offset++];
            mapHeader.Weather = rom.Data[offset++];
            mapHeader.Type = rom.Data[offset++];
            mapHeader.SinUso1 = rom.Data[offset++];
            mapHeader.SinUso2 = rom.Data[offset++];
            mapHeader.LabelToggle = rom.Data[offset++];
            mapHeader.SinUso3 = rom.Data[offset++];

            return mapHeader;
        }
    }
}
