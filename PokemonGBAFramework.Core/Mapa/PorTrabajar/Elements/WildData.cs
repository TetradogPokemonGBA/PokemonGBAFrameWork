using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class WildData : ICloneable
	{
		public enum Type
		{
			Water,Grass,Tree,Fishing
		}
		public WildPokemonData[] aWildPokemon = new WildPokemonData[4];
		public WildDataHeader wildDataHeader;


		public WildData(RomGba rom, WildDataHeader h)
		{

			wildDataHeader = h;


			if (h.PGrass != default)
				aWildPokemon[0] = new WildPokemonData(rom, WildData.Type.Grass,(int)h.PGrass);

			if (h.PWater != default)
				aWildPokemon[1] = new WildPokemonData(rom, WildData.Type.Water, (int)h.PWater);

			if (h.PTrees != default)
				aWildPokemon[2] = new WildPokemonData(rom, WildData.Type.Tree, (int)h.PTrees);

			if (h.PFishing != default)
				aWildPokemon[3] = new WildPokemonData(rom, WildData.Type.Fishing, (int)h.PFishing);
		}

		public WildData(int bank, int map, OffsetRom offsetGrass, OffsetRom offsetWater, OffsetRom offsetTree, OffsetRom offsetFishing)
		{
			wildDataHeader = new WildDataHeader(bank, map, offsetGrass, offsetWater, offsetTree, offsetFishing);
		}

		public WildData(WildData d)
		{
			if (d.wildDataHeader is ICloneable)
				this.wildDataHeader = (WildDataHeader)d.wildDataHeader.Clone();

			for (int i = 0; i < d.aWildPokemon.Length; i++)
				if (d.aWildPokemon[i] is ICloneable)
					aWildPokemon[i] = (WildPokemonData)d.aWildPokemon[i].Clone();


		}

		public void addWildData(Type t)
		{
			addWildData(t, (byte)0x15);
		}

		public void removeWildData(RomGba rom,Type t)
		{
			WildPokemonData d = null;
			if (aWildPokemon[(int)t] == null)
				return;
			int size;
			int pkmnData;
			switch (t)
			{
				case Type.Water:
					d = aWildPokemon[1];
					size = d.getWildDataSize();
					pkmnData = (int)d.PPokemonData;
					rom.Data.Remove((int)wildDataHeader.PWater,WildPokemonData.LENGTH);
					wildDataHeader.PWater = default;
					aWildPokemon[1] = null;
					break;
				case Type.Tree:
					d = aWildPokemon[2];
					size = d.getWildDataSize();
					pkmnData = (int)d.PPokemonData;
					rom.Data.Remove((int)wildDataHeader.PTrees,  WildPokemonData.LENGTH);
					wildDataHeader.PTrees = default;
					aWildPokemon[2] = null;
					break;
				case Type.Fishing:
					d = aWildPokemon[3];
					size = d.getWildDataSize();
					pkmnData = (int)d.PPokemonData;
					rom.Data.Remove((int)wildDataHeader.PFishing, WildPokemonData.LENGTH);
					wildDataHeader.PFishing = default;
					aWildPokemon[3] = null;
					break;
				case Type.Grass:
				default:
					d = aWildPokemon[0];
					size = d.getWildDataSize();
					pkmnData = (int)d.PPokemonData;
					rom.Data.Remove((int)wildDataHeader.PGrass,WildPokemonData.LENGTH);
					wildDataHeader.PGrass = default;
					aWildPokemon[0] = null;
					break;
			}
			rom.Data.Remove(pkmnData, size);
		}

		public void addWildData(Type t, byte ratio)
		{
			WildPokemonData d = new WildPokemonData(t,ratio);
			switch (t)
			{
				case Type.Grass:
					aWildPokemon[0] = d;
					break;
				case Type.Water:
					aWildPokemon[1] = d;
					break;
				case Type.Tree:
					aWildPokemon[2] = d;
					break;
				case Type.Fishing:
					aWildPokemon[3] = d;
					break;
			}
		}

		//public void save(RomGba rom,int headerloc)
		//{
		//	if (aWildPokemon[0].aWildPokemon != null)
		//	{
		//		if (wildDataHeader.pGrass == 0 || wildDataHeader.pGrass > 0x1FFFFFF)
		//			wildDataHeader.pGrass = rom.findFreespace(DataStore.FreespaceStart, 8);
		//		rom.floodBytes((int)wildDataHeader.pGrass, (byte)0, 8); //Prevent these bytes from being used by wild data
		//		rom.Data.SetArray((int)wildDataHeader.pGrass, aWildPokemon[0].GetBytes());
				
		//	}
		//	if (aWildPokemon[1].aWildPokemon != null)
		//	{
		//		if (wildDataHeader.pWater == 0 || wildDataHeader.pWater > 0x1FFFFFF)
		//			wildDataHeader.pWater = rom.findFreespace(DataStore.FreespaceStart, 8);
		//		rom.floodBytes((int)wildDataHeader.pWater, (byte)0, 8); //Prevent these bytes from being used by wild data
		//		rom.Data.SetArray((int)wildDataHeader.pWater, aWildPokemon[1].GetBytes());
		//	}
		//	if (aWildPokemon[2].aWildPokemon != null)
		//	{
		//		if (wildDataHeader.pTrees == 0 || wildDataHeader.pTrees > 0x1FFFFFF)
		//			wildDataHeader.pTrees = rom.findFreespace(DataStore.FreespaceStart, 8);
		//		rom.floodBytes((int)wildDataHeader.pTrees, (byte)0, 8); //Prevent these bytes from being used by wild data
		//		rom.Data.SetArray((int)wildDataHeader.pTrees, aWildPokemon[2].GetBytes());
		//	}
		//	if (aWildPokemon[3].aWildPokemon != null)
		//	{
		//		if (wildDataHeader.pFishing == 0 || wildDataHeader.pFishing > 0x1FFFFFF)
		//			wildDataHeader.pFishing = rom.findFreespace(DataStore.FreespaceStart, 8);
		//		rom.floodBytes((int)wildDataHeader.pFishing, (byte)0, 8); //Prevent these bytes from being used by wild data
		//		rom.Data.SetArray((int)wildDataHeader.pFishing, aWildPokemon[3].GetBytes());
		//	}
		//	wildDataHeader.save(headerloc);
		//}

		public Object Clone()
		{
			return new WildData(this);
		}
	}

}
