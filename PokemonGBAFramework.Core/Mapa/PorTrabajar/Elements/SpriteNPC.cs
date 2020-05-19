using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class SpriteNPC:SpriteBase
    {
        public const int LENGTH = 24;
        public SpriteNPC() : this(0, 0) { }
        public SpriteNPC(byte x, byte y):base(x,y)
        {
            B1 = 0;
            SpriteSet = 0;
            B4 = 0;
            B6 = 0;
            B8 = 0;
            B9 = 0;
            Behavior1 = 0;
            B10 = 0;
            Behavior2 = 0;
            IsTrainer = 0;
            B14 = 0;
            TrainerLOS = 0;
            B16 = 0;
            OffsetScript =default;
            Flag = 0;
            B23 = 0;
            B24 = 0;
        }

        public byte B1 { get; set; }
        public Word SpriteSet { get; set; }
        public byte B4 { get; set; }
        public byte B6 { get; set; }
        public byte B8 { get; set; }
        public byte B9 { get; set; }
        public byte Behavior1 { get; set; }
        public byte B10 { get; set; }
        public byte Behavior2 { get; set; }
        public byte IsTrainer { get; set; }
        public byte B14 { get; set; }
        public byte TrainerLOS { get; set; }
        public byte B16 { get; set; }
        public OffsetRom OffsetScript { get; set; }
        public Word Flag { get; set; }
        public byte B23 { get; set; }
        public byte B24 { get; set; }

        public override byte[] GetBytes()
        {
            byte[] data = new byte[LENGTH];
            data[0] = B1;
            Word.SetData(data, 1, SpriteSet);
            data[3] = B4;
            data[4] = X;
            data[5] = B6;
            data[6] = Y;
            data[7] = B8;
            data[8] = B9;
            data[9] = Behavior1;
            data[10] = B10;
            data[11] = Behavior2;
            data[12] = IsTrainer;
            data[13] = B14;
            data[14] = TrainerLOS;
            data[15] = B16;
            OffsetRom.Set(data, 16,new OffsetRom(OffsetScript + (OffsetScript == 0 ? 0 : 0x08000000)));
            Word.SetData(data, 20, Flag);
            data[22] = B23;
            data[23] = B24;

            return data;
        }
        public static SpriteNPC Get(ScriptManager scriptManager,RomGba rom, int offset)
        {
            SpriteNPC spriteNPC = new SpriteNPC();
            spriteNPC.B1 = rom.Data[offset++];
            spriteNPC.SpriteSet = new Word(rom, offset);
            offset += Word.LENGTH;
            spriteNPC.B4 = rom.Data[offset++];
            spriteNPC.X = rom.Data[offset++];
            spriteNPC.B6 = rom.Data[offset++];
            spriteNPC.Y = rom.Data[offset++];
            spriteNPC.B8 = rom.Data[offset++];
            spriteNPC.B9 = rom.Data[offset++];
            spriteNPC.Behavior1 = rom.Data[offset++];
            spriteNPC.B10 = rom.Data[offset++];
            spriteNPC.Behavior2 = rom.Data[offset++];
            spriteNPC.IsTrainer = rom.Data[offset++];
            spriteNPC.B14 = rom.Data[offset++];
            spriteNPC.TrainerLOS = rom.Data[offset++];
            spriteNPC.B16 = rom.Data[offset++];
            spriteNPC.OffsetScript = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            spriteNPC.Flag = new Word(rom, offset);
            offset += Word.LENGTH;
            spriteNPC.B23 = rom.Data[offset++];
            spriteNPC.B24 = rom.Data[offset++];

            if (spriteNPC.OffsetScript.IsEmpty)
                spriteNPC.OffsetScript = default;
            else spriteNPC.OffsetScript.Fix();


            return spriteNPC;
        }

 
    }
}
