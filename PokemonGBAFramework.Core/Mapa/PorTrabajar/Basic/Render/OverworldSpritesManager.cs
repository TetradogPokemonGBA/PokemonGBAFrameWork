using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic.Render
{
	public class OverworldSpritesManager
	{
		public static OverworldSprites[] Sprites = new OverworldSprites[256];


		public static Bitmap GetImage(RomGba rom, int index, int offsetSpriteBase, int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{
			if (Sprites[index] != null)
				return Sprites[index].imgBuffer;
			else
				return loadSprite(rom, index, offsetSpriteBase, spriteColors, sizeSmall, sizeNormal, sizeLarge).imgBuffer;
		}

		public static OverworldSprites GetSprite(RomGba rom, int index, int offsetSpriteBase, int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{
			if (Sprites[index] != null)
				return Sprites[index];
			else
				return loadSprite(rom, index,offsetSpriteBase, spriteColors, sizeSmall, sizeNormal, sizeLarge);
		}


		public static OverworldSprites loadSprite(RomGba rom, int num,int offsetSpriteBase, int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{

			int ptr = new OffsetRom(rom, offsetSpriteBase + (num * 4));
			Sprites[num] = new OverworldSprites(rom, ptr, spriteColors, sizeSmall, sizeNormal, sizeLarge);
			return Sprites[num];
		}


		public void run(RomGba rom,int mehSettingShowSprites,int numSprites, int offsetSpriteBase, int spriteColors, int sizeSmall, int sizeNormal, int sizeLarge)
		{
			if (mehSettingShowSprites != 0)// Don't load if not enabled.
				for (int i = 0; i < numSprites; i++)
				{
					loadSprite(rom, i, offsetSpriteBase, spriteColors, sizeSmall, sizeNormal, sizeLarge);
				}
		}
	}

}
