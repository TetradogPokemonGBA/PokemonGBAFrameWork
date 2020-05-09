using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class WildData : ICloneable,IClonable<WildData>
	{
		public enum Type
		{
			Grass,Water,Tree,Fishing
		}
		public static readonly int NumWildAreas = Enum.GetNames(typeof(Type)).Length;
		public WildPokemonData[] WildArea { get; set; } = new WildPokemonData[NumWildAreas];
		public WildDataHeader WildDataHeader { get; set; }

		public WildData() { }


		public WildData(int bank, int map, OffsetRom offsetGrass, OffsetRom offsetWater, OffsetRom offsetTree, OffsetRom offsetFishing)
		{
			WildDataHeader = new WildDataHeader(bank, map, offsetGrass, offsetWater, offsetTree, offsetFishing);
		}

		public WildData(WildData d)
		{
			if (d.WildDataHeader is ICloneable)
				this.WildDataHeader = (WildDataHeader)d.WildDataHeader.Clone();

			for (int i = 0; i < d.WildArea.Length; i++)
				if (d.WildArea[i] is ICloneable)
					WildArea[i] = (WildPokemonData)d.WildArea[i].Clone();


		}

		public void Add(Type wildAreaType)
		{
			Add(wildAreaType, (byte)0x15);
		}

		public void Remove(RomGba rom,Type tipoWildData)
		{
			int size;
			int pkmnData;
			int position;
			OffsetRom offset;
			WildPokemonData wildPokemonData;

			if (WildArea[(int)tipoWildData] != default)
			{
				switch (tipoWildData)
				{
					case Type.Water:
						position = 1;
						offset = WildDataHeader.OffsetWater;
						WildDataHeader.OffsetWater = default;
						break;
					case Type.Tree:
						position = 2;
						offset = WildDataHeader.OffsetTrees;
						WildDataHeader.OffsetTrees = default;
			
						break;
					case Type.Fishing:
						position = 3;
						offset = WildDataHeader.OffsetFishing;
						WildDataHeader.OffsetFishing = default;
						break;
					default:
						position = 0;
						offset = WildDataHeader.OffsetGrass;
						WildDataHeader.OffsetGrass = default;
						break;
				}

				wildPokemonData = WildArea[position];
				size = wildPokemonData.Size;
				pkmnData = (int)wildPokemonData.OffsetPokemonData;
				rom.Data.Remove((int)offset, WildPokemonData.LENGTH);
				WildArea[position] = default;

				rom.Data.Remove(pkmnData, size);
			}
		}

		public void Add(Type tipoWildArea, byte ratio)
		{
			WildPokemonData wildAreaData = new WildPokemonData(tipoWildArea,ratio);
			switch (tipoWildArea)
			{
				case Type.Grass:
					WildArea[0] = wildAreaData;
					break;
				case Type.Water:
					WildArea[1] = wildAreaData;
					break;
				case Type.Tree:
					WildArea[2] = wildAreaData;
					break;
				case Type.Fishing:
					WildArea[3] = wildAreaData;
					break;
			}
		}



		public Object Clone() => Clon();
		public WildData Clon() => new WildData(this);

		public static WildData Get(RomGba rom, WildDataHeader wildDataHeader)
		{
			WildData wildData = new WildData();
			wildData.WildDataHeader = wildDataHeader;


			if (!Equals(wildDataHeader.OffsetGrass, default))
				wildData.WildArea[0] =  WildPokemonData.Get(rom, WildData.Type.Grass, wildDataHeader.OffsetGrass);

			if (!Equals(wildDataHeader.OffsetWater, default))
				wildData.WildArea[1] = WildPokemonData.Get(rom, WildData.Type.Water, wildDataHeader.OffsetWater);

			if (!Equals(wildDataHeader.OffsetTrees, default))
				wildData.WildArea[2] = WildPokemonData.Get(rom, WildData.Type.Tree, wildDataHeader.OffsetTrees);

			if (!Equals(wildDataHeader.OffsetFishing, default))
				wildData.WildArea[3] = WildPokemonData.Get(rom, WildData.Type.Fishing, wildDataHeader.OffsetFishing);
			return wildData;
		}
	}

}
