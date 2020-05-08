using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class SpriteExit
    {
        public const int LENGTH = 8;

        public SpriteExit(RomGba rom, int offset)
        {
            X = rom.Data[offset++];
            B2 = rom.Data[offset++];
            Y = rom.Data[offset++];
            B4 = rom.Data[offset++];
            B5 = rom.Data[offset++];
            B6 = rom.Data[offset++];
            Map = rom.Data[offset++];
            Bank = rom.Data[offset++];
        }

        public SpriteExit(byte x, byte y)
        {

            X = x;
            Y = y;
            B2 = 0;
            B4 = 0;
            B5 = 0;
            B6 = 0;
            Map = 0;
            Bank = 0;
        }


        public byte X { get; set; }
        public byte B2 { get; set; }
        public byte Y { get; set; }
        public byte B4 { get; set; }
        public byte B5 { get; set; }
        public byte B6 { get; set; }
        public byte Map { get; set; }
        public byte Bank { get; set; }


        public byte[] GetBytes()
        {
            return new byte[] {X,B2,Y,B4,B5,B6,Map,Bank };
        }
    }

}
