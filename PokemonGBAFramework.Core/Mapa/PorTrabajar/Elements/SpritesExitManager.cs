using PokemonGBAFramework.Core.Mapa.Basic;
using PokemonGBAFramework.Core.Mapa.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa
{
	public class SpritesExitManager : BaseManager<SpriteExit>
	{
		public SpritesExitManager(RomGba rom, int offset, int count) : base(rom, offset, count)
		{
		}

		protected override int LengthSingelItem => SpriteExit.LENGTH;
		
		protected override SpriteExit IGet(ScriptManager scriptManager,RomGba rom, int offset)
		{
			return SpriteExit.Get(rom, offset);
		}

		protected override SpriteExit IGetNew(int x, int y)
		{
			return new SpriteExit((byte)x, (byte)y);
		}
	}
}
