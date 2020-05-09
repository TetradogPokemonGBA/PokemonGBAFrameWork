using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class SpritesSignManager : BaseManager<SpriteSign>
	{
		public SpritesSignManager(RomGba rom, int offset, int count) : base(rom, offset, count)
		{
		}
		protected override int LengthSingelItem => SpriteSign.LENGTH;
		protected override SpriteSign IGetNew(int x, int y)
		{
			return new SpriteSign((byte)x,(byte) y);
		}
		protected override SpriteSign IGet(RomGba rom, int offset)
		{
			return SpriteSign.Get(rom, offset);
		}
	}

}
