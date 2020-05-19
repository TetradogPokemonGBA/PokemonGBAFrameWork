using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class SpritesNPCManager:BaseManager<SpriteNPC>
    {
        public SpritesNPCManager(RomGba rom, int offset, int count):base(rom,offset,count)
        {
            
        }

        protected override int LengthSingelItem => SpriteNPC.LENGTH;

       
        public int[] GetIndices()
        {
            int[] indices = new int[Items.Count];
            for (int i = 0; i < Items.Count; i++)
            {
                indices[i] = Items[i].SpriteSet;
            }
            return indices;
        }

        protected override SpriteNPC IGet(ScriptManager scriptManager,RomGba rom, int offset)
        {
            return Get(rom, offset);
        }

        protected override SpriteNPC IGetNew(int x, int y)
        {
            return GetNew((byte)x,(byte) y);
        }
        public static SpriteNPC Get(ScriptManager scriptManager,RomGba rom, int offset)
        {
            return SpriteNPC.Get(rom, offset);
        }
        public static SpriteNPC GetNew(int x, int y)
        {
            return new SpriteNPC((byte)x, (byte)y);
        }
    }

}
