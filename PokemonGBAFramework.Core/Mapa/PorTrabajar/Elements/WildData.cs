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
			Water,Grass,Tree,Fishing
		}

		public WildPokemonData[] WildArea { get; set; } = new WildPokemonData[Enum.GetNames(typeof(Type)).Length];
		public WildDataHeader WildDataHeader { get; set; }


		public WildData(RomGba rom, WildDataHeader wildDataHeader)
		{

			WildDataHeader = wildDataHeader;


			if (wildDataHeader.PGrass != default)
				WildArea[0] = new WildPokemonData(rom, WildData.Type.Grass,(int)wildDataHeader.PGrass);

			if (wildDataHeader.PWater != default)
				WildArea[1] = new WildPokemonData(rom, WildData.Type.Water, (int)wildDataHeader.PWater);

			if (wildDataHeader.PTrees != default)
				WildArea[2] = new WildPokemonData(rom, WildData.Type.Tree, (int)wildDataHeader.PTrees);

			if (wildDataHeader.PFishing != default)
				WildArea[3] = new WildPokemonData(rom, WildData.Type.Fishing, (int)wildDataHeader.PFishing);
		}

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
						offset = WildDataHeader.PWater;
						WildDataHeader.PWater = default;
						break;
					case Type.Tree:
						position = 2;
						offset = WildDataHeader.PTrees;
						WildDataHeader.PTrees = default;
			
						break;
					case Type.Fishing:
						position = 3;
						offset = WildDataHeader.PFishing;
						WildDataHeader.PFishing = default;
						break;
					default:
						position = 0;
						offset = WildDataHeader.PGrass;
						WildDataHeader.PGrass = default;
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
	}

}
