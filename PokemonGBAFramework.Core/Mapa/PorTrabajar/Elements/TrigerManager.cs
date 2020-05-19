using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class TriggerManager : BaseManager<Trigger>
    {
        public TriggerManager(RomGba rom, int offset, int count) : base(rom, offset, count)
        {
        }

        protected override int LengthSingelItem => Trigger.LENGTH;

        protected override Trigger IGet(ScriptManager scriptManager,RomGba rom, int offset)
        {
            return Trigger.Get(rom, offset);
        }

        protected override Trigger IGetNew(int x, int y)
        {
            return new Trigger((byte)x, (byte)y);
        }
    }

}
