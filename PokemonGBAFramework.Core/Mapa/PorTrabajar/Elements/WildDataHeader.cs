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


		public WildDataHeader(RomGba rom, int offset) : this(rom.Data.Bytes, offset) { }

		public WildDataHeader(byte[] rom, int offset = 0)
		{
			const int FILLERBYTES = 2;

			BBank = rom[offset++];
			BMap = rom[offset++];
			offset += FILLERBYTES;
			PGrass = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			PWater = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			PTrees = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			PFishing = new OffsetRom(rom, offset);
		}
		public WildDataHeader(int bank, int map, OffsetRom offsetGrass, OffsetRom offsetWater, OffsetRom offsetTree, OffsetRom offsetFishing)
		{
			BBank = (byte)bank;
			BMap = (byte)map;
			PGrass = offsetGrass;
			PWater = offsetWater;
			PTrees = offsetTree;
			PFishing = offsetFishing;
		}


		public byte BBank { get; set; }
		public byte BMap { get; set; }
		public OffsetRom PGrass { get; set; }
		public OffsetRom PWater { get; set; }
		public OffsetRom PTrees { get; set; }
		public OffsetRom PFishing { get; set; }

		public byte[] GetBytes()
		{
			const int FILLERBYTES = 2;
			byte[] emptyPointer = new byte[OffsetRom.LENGTH];
			return new byte[] { BBank, BMap }.AddArray(new byte[FILLERBYTES],
													   PGrass != default ? PGrass.BytesPointer : emptyPointer,
													   PWater != default ? PWater.BytesPointer : emptyPointer,
													   PTrees != default ? PTrees.BytesPointer : emptyPointer, 
													   PFishing != default ? PFishing.BytesPointer : emptyPointer
													    );

		}

		public object Clone()
		{
			return Clon();
		}

		public WildDataHeader Clon()
		{
			return new WildDataHeader(GetBytes());
		}
	}

}
