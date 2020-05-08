using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic.Render
{
	public class OverworldSpritesManager
	{
		public  OverworldSprites[] Sprites = new OverworldSprites[256];


		public  Bitmap GetImage(RomGba rom, int index, int offsetSpriteBase, int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{
			if (Sprites[index] != null)
				return Sprites[index].imgBuffer;
			else
				return loadSprite(rom, index, offsetSpriteBase, spriteColors, sizeSmall, sizeNormal, sizeLarge).imgBuffer;
		}

		public  OverworldSprites GetSprite(RomGba rom, int index, int offsetSpriteBase, int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{
			if (Sprites[index] != null)
				return Sprites[index];
			else
				return loadSprite(rom, index,offsetSpriteBase, spriteColors, sizeSmall, sizeNormal, sizeLarge);
		}


		public  OverworldSprites loadSprite(RomGba rom, int num,int offsetSpriteBase, int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{

			int ptr = new OffsetRom(rom, offsetSpriteBase + (num * 4));
			Sprites[num] = new OverworldSprites(rom, ptr, spriteColors, sizeSmall, sizeNormal, sizeLarge);
			return Sprites[num];
		}

	}

}
