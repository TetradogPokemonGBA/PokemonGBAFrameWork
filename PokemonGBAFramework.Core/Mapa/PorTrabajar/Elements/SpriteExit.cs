using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class SpriteExit:SpriteBase
    {
        public const int LENGTH = 8;

        public SpriteExit() : this(0, 0) { }
        public SpriteExit(byte x, byte y):base(x,y)
        {
            B2 = 0;
            B4 = 0;
            B5 = 0;
            B6 = 0;
            Map = 0;
            Bank = 0;
        }
        public byte B2 { get; set; }
        public byte B4 { get; set; }
        public byte B5 { get; set; }
        public byte B6 { get; set; }
        public byte Map { get; set; }
        public byte Bank { get; set; }


        public override byte[] GetBytes()
        {
            return new byte[] {X,B2,Y,B4,B5,B6,Map,Bank };
        }

        public static SpriteExit Get(ScriptAndASMManager scriptManager,RomGba rom, int offset)
        {
            SpriteExit spriteExit = new SpriteExit();
            spriteExit.X = rom.Data[offset++];
            spriteExit.B2 = rom.Data[offset++];
            spriteExit.Y = rom.Data[offset++];
            spriteExit.B4 = rom.Data[offset++];
            spriteExit.B5 = rom.Data[offset++];
            spriteExit.B6 = rom.Data[offset++];
            spriteExit.Map = rom.Data[offset++];
            spriteExit.Bank = rom.Data[offset++];
            return spriteExit;
        }
    }

}
