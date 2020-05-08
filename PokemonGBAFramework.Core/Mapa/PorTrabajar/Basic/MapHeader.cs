using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class MapHeader
    {
        int hdrSize;//This is internal and does not go into the ROM
        public MapHeader(RomGba rom, int offset)
        {
            int offsetOri = BOffset;
            BOffset = offset & 0x1FFFFFF;

            OffsetMap = new OffsetRom(rom, BOffset);
            BOffset += OffsetRom.LENGTH;
            OffsetSprites = new OffsetRom(rom, BOffset);
            BOffset += OffsetRom.LENGTH;
            OffsetScript = new OffsetRom(rom, BOffset);
            BOffset += OffsetRom.LENGTH;
            OffsetConnect = new OffsetRom(rom, BOffset);
            BOffset += OffsetRom.LENGTH;
            Song = new Word(rom, BOffset);
            BOffset += Word.LENGTH;
            Map = new Word(rom, BOffset);
            BOffset += Word.LENGTH;

            LabelID = rom.Data[BOffset++];
            Flash = rom.Data[BOffset++];
            Weather = rom.Data[BOffset++];
            Type = rom.Data[BOffset++];
            BUnused1 = rom.Data[BOffset++];
            BUnused2 = rom.Data[BOffset++];
            LabelToggle = rom.Data[BOffset++];
            BUnused3 = rom.Data[BOffset++];


            hdrSize = BOffset - offsetOri - 0x8000000;
        }

        public int BOffset { get; set; }
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
        public byte BUnused1 { get; set; }
        public byte BUnused2 { get; set; }
        public byte LabelToggle { get; set; }
        public byte BUnused3 { get; set; }


        //public void save()
        //{
        //	rom.Seek(bOffset);
        //	rom.writePointer((int)pMap);
        //	rom.writePointer((int)pSprites);
        //	rom.writePointer((int)pScript);
        //	rom.writePointer((int)pConnect);
        //	rom.writeWord(hSong);
        //	rom.writeWord(hMap);

        //	rom.writeByte(bLabelID);
        //	rom.writeByte(bFlash);
        //	rom.writeByte(bWeather);
        //	rom.writeByte(bType);
        //	rom.writeByte(bUnused1);
        //	rom.writeByte(bUnused2);
        //	rom.writeByte(bLabelToggle);
        //	rom.writeByte(bUnused3);
        //}
    }
}
