using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class SpritesSignManager
	{
	public List<SpriteSign> mapSigns;
	private int internalOffset;
	private int originalSize;
	private Map loadedMap;


		public int getSpriteIndexAt(int x, int y)
		{
			const int MARCAFIN = -1;
			int pos = MARCAFIN;

			for (int i = 0; i < mapSigns.Count && pos == MARCAFIN; i++)
			{
				if (mapSigns[i].BX == x && mapSigns[i].BY == y)
				{
					pos = i;
				}
				i++;
			}

			return pos;

		}

	public SpritesSignManager(RomGba rom, Map m, int offset, int count)
	{
		internalOffset = offset;
		this.loadedMap = m;

		mapSigns = new List<SpriteSign>();
		int i = 0;
		for (i = 0; i < count; i++)
		{
			mapSigns.Add(new SpriteSign(rom,offset));
				offset += SpriteSign.LENGTH;
		}
		originalSize = getSize();
	}

	public int getSize()
	{
		return mapSigns.Count * SpriteSign.LENGTH;
	}

	public void add(int x, int y)
	{
		mapSigns.Add(new SpriteSign((byte)x, (byte)y));
	}

	public void remove(int x, int y)
	{
		mapSigns.RemoveAt(getSpriteIndexAt(x, y));
	}

	//public void save()
	//{
	//	rom.floodBytes(BitConverter.shortenPointer(internalOffset), rom.freeSpaceByte, originalSize);

	//	//TODO make this a setting, ie always repoint vs keep pointers
	//	if (originalSize < getSize())
	//	{
	//		internalOffset = rom.findFreespace(DataStore.FreespaceStart, getSize());

	//		if (internalOffset < 0x08000000)
	//			internalOffset += 0x08000000;
	//	}
	//	loadedMap.mapSprites.pSigns = internalOffset & 0x1FFFFFF;
	//	loadedMap.mapSprites.bNumSigns = (byte)mapSigns.size();

	//	rom.Seek(internalOffset);
	//	for (SpriteSign s : mapSigns)
	//		s.save();
	//}
}

}
