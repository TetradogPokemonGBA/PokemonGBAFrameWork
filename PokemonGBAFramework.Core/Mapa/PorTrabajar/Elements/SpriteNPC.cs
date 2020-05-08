using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class SpriteNPC
    {
        public const int LENGTH = 24;
        public SpriteNPC(RomGba rom, int offset)
        {

            B1 = rom.Data[offset++];
            BSpriteSet = new Word(rom, offset);
            offset += Word.LENGTH;
            B4 = rom.Data[offset++];
            BX = rom.Data[offset++];
            B6 = rom.Data[offset++];
            BY = rom.Data[offset++];
            B8 = rom.Data[offset++];
            B9 = rom.Data[offset++];
            BBehavior1 = rom.Data[offset++];
            B10 = rom.Data[offset++];
            BBehavior2 = rom.Data[offset++];
            BIsTrainer = rom.Data[offset++];
            B14 = rom.Data[offset++];
            BTrainerLOS = rom.Data[offset++];
            B16 = rom.Data[offset++];
            PScript = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            IFlag = new Word(rom, offset);
            offset += Word.LENGTH;
            B23 = rom.Data[offset++];
            B24 = rom.Data[offset++];
        }

        public SpriteNPC(byte x, byte y)
        {
            B1 = 0;
            BSpriteSet = 0;
            B4 = 0;
            BX = x;
            B6 = 0;
            BY = y;
            B8 = 0;
            B9 = 0;
            BBehavior1 = 0;
            B10 = 0;
            BBehavior2 = 0;
            BIsTrainer = 0;
            B14 = 0;
            BTrainerLOS = 0;
            B16 = 0;
            PScript = new OffsetRom(0);
            IFlag = 0;
            B23 = 0;
            B24 = 0;
        }

        public byte B1 { get; set; }
        public Word BSpriteSet { get; set; }
        public byte B4 { get; set; }
        public byte BX { get; set; }
        public byte B6 { get; set; }
        public byte BY { get; set; }
        public byte B8 { get; set; }
        public byte B9 { get; set; }
        public byte BBehavior1 { get; set; }
        public byte B10 { get; set; }
        public byte BBehavior2 { get; set; }
        public byte BIsTrainer { get; set; }
        public byte B14 { get; set; }
        public byte BTrainerLOS { get; set; }
        public byte B16 { get; set; }
        public OffsetRom PScript { get; set; }
        public Word IFlag { get; set; }
        public byte B23 { get; set; }
        public byte B24 { get; set; }

        public byte[] GetBytes()
        {
            byte[] data = new byte[LENGTH];
            data[0] = B1;
            Word.SetData(data, 1, BSpriteSet);
            data[3] = B4;
            data[4] = BX;
            data[5] = B6;
            data[6] = BY;
            data[7] = B8;
            data[8] = B9;
            data[9] = BBehavior1;
            data[10] = B10;
            data[11] = BBehavior2;
            data[12] = BIsTrainer;
            data[13] = B14;
            data[14] = BTrainerLOS;
            data[15] = B16;
            OffsetRom.SetOffset(data, 16,new OffsetRom(PScript + (PScript == 0 ? 0 : 0x08000000)));
            Word.SetData(data, 20, IFlag);
            data[22] = B23;
            data[23] = B24;

            return data;
        }
    }
}
