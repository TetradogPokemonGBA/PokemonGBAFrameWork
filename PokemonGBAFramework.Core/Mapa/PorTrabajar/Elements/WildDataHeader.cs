using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class WildDataHeader : ICloneable,IClonable<WildDataHeader>
	{
		public const int LENGTH = 20;

		public WildDataHeader() { }

		public WildDataHeader(int bank, int map, OffsetRom offsetGrass, OffsetRom offsetWater, OffsetRom offsetTree, OffsetRom offsetFishing)
		{
			Bank = (byte)bank;
			Map = (byte)map;
			OffsetGrass = offsetGrass;
			OffsetWater = offsetWater;
			OffsetTrees = offsetTree;
			OffsetFishing = offsetFishing;
		}


		public byte Bank { get; set; }
		public byte Map { get; set; }
		public OffsetRom OffsetGrass { get; set; }
		public OffsetRom OffsetWater { get; set; }
		public OffsetRom OffsetTrees { get; set; }
		public OffsetRom OffsetFishing { get; set; }

		public byte[] GetBytes()
		{
			const int FILLERBYTES = 2;
			byte[] emptyPointer = new byte[OffsetRom.LENGTH];
			return new byte[] { Bank, Map }.AddArray(new byte[FILLERBYTES],
													!Equals(OffsetGrass,default) ? OffsetGrass.BytesPointer : emptyPointer,
													!Equals(OffsetWater, default) ? OffsetWater.BytesPointer : emptyPointer,
													!Equals(OffsetTrees, default) ? OffsetTrees.BytesPointer : emptyPointer,
													!Equals(OffsetFishing, default) ? OffsetFishing.BytesPointer : emptyPointer
													    );

		}

		public object Clone()
		{
			return Clon();
		}

		public WildDataHeader Clon()
		{
			return  Get(GetBytes());
		}
		public static WildDataHeader Get(ScriptManager scriptManager,RomGba rom, int offset) => Get(rom.Data.Bytes, offset);

		public static WildDataHeader Get(byte[] rom, int offset = 0)
		{
			const int FILLERBYTES = 2;

			WildDataHeader wildDataHeader = new WildDataHeader();
			wildDataHeader.Bank = rom[offset++];
			wildDataHeader.Map = rom[offset++];
			offset += FILLERBYTES;
			wildDataHeader.OffsetGrass = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			wildDataHeader.OffsetWater = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			wildDataHeader.OffsetTrees = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			wildDataHeader.OffsetFishing = new OffsetRom(rom, offset);

			if (wildDataHeader.OffsetGrass.IsEmpty)
				wildDataHeader.OffsetGrass = default;
			else wildDataHeader.OffsetGrass.Fix();

			if (wildDataHeader.OffsetWater.IsEmpty)
				wildDataHeader.OffsetWater = default;
			else wildDataHeader.OffsetWater.Fix();

			if (wildDataHeader.OffsetTrees.IsEmpty)
				wildDataHeader.OffsetTrees = default;
			else wildDataHeader.OffsetTrees.Fix();

			if (wildDataHeader.OffsetFishing.IsEmpty)
				wildDataHeader.OffsetFishing = default;
			else wildDataHeader.OffsetFishing.Fix();

			return wildDataHeader;
		}
	}

}
