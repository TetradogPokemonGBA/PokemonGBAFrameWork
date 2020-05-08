using PokemonGBAFramework.Core.Mapa.Basic;
using PokemonGBAFramework.Core.Mapa.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa
{
	public class SpritesExitManager
	{
	public List<SpriteExit> mapExits;
	private Map loadedMap;
	private int internalOffset = 0;
	private int originalSize;


	public int getSpriteIndexAt(int x, int y)
	{
		int i = 0;
		foreach (SpriteExit exit in mapExits)
		{
			if (exit.X == x && exit.Y == y)
			{
				return i;
			}
			i++;
		}

		return -1;

	}

	public SpritesExitManager(RomGba rom, Map m, int offset, int count)
	{

		mapExits = new List<SpriteExit>();
		int i = 0;
		for (i = 0; i < count; i++)
		{
			mapExits.Add(new SpriteExit(rom,offset));
				offset += SpriteExit.LENGTH;

		}
		originalSize = getSize();
		internalOffset = offset;
		this.loadedMap = m;
	}

	public int getSize()
	{
		return mapExits.Count * SpriteExit.LENGTH;
	}

	public void add(int x, int y)
	{
		mapExits.Add(new SpriteExit((byte)x, (byte)y));
	}

	public void remove(int x, int y)
	{
		mapExits.RemoveAt(getSpriteIndexAt(x, y));
	}

	//public void save(RomGba rom)
	//{
	//	rom.floodBytes(BitConverter.shortenPointer(internalOffset), rom.freeSpaceByte, originalSize);

	//	//TODO make this a setting, ie always repoint vs keep pointers
	//	int i = getSize();
	//	if (originalSize < getSize())
	//	{
	//		internalOffset = rom.Data.SearchEmptyBytes(getSize());

	//		if (internalOffset < 0x08000000)
	//			internalOffset += 0x08000000;
	//	}

	//	loadedMap.mapSprites.PExits = internalOffset & 0x1FFFFFF;
	//	loadedMap.mapSprites.bNumExits = (byte)mapExits.Count;

		
	//	foreach (SpriteExit mapExit in mapExits)
	//		{
	//			rom.Data.SetArray(internalOffset, mapExit.GetBytes());
	//			internalOffset += SpriteExit.LENGTH;
	//		}
	//}
}
}
