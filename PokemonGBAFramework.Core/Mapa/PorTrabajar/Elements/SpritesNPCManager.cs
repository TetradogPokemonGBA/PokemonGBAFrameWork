using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class SpritesNPCManager:BaseManager<SpriteNPC>
    {
        public SpritesNPCManager(RomGba rom, int offset, int count):base(rom,offset,count)
        {
            
        }

        protected override int LengthSingelItem => SpriteNPC.LENGTH;

       
        public IEnumerable<Word> GetIndices()
        {
            return Items.Select(i => i.SpriteSet);
        }

        protected override SpriteNPC IGet(RomGba rom, int offset)
        {
            return SpriteNPC.Get(rom, offset);
        }

        protected override SpriteNPC IGetNew(int x, int y)
        {
            return new SpriteNPC((byte)x, (byte)y);
        }

    }

}
