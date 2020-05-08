using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
namespace PokemonGBAFramework.Core.Mapa
{
	public class Connection
	{
		public const int LENGTH=12;

		public enum Type
		{
			NULL,
			DOWN,
			UP,
			LEFT,
			RIGHT,
			DIVE,
			EMERGE
		}



		public Connection(RomGba rom,int offset)
		{
			load(rom,offset);
		}

		public Connection(Type c, byte bank, byte map)
		{

			LType = c;
			Offset = default;
			Bank = bank;
			Map = map;
			Filler = 0;
		}
		public Type LType { get; set; }
		public OffsetRom Offset { get; set; }
		public byte Bank { get; set; }
		public byte Map { get; set; }
		public Word Filler { get; set; }
		public void load(RomGba rom,int offset)
		{
			LType = (Type)Serializar.ToLong(rom.Data.Bytes.SubArray(sizeof(long)));
			offset += sizeof(long);
			Offset = new OffsetRom(rom,offset);
			offset += OffsetRom.LENGTH;
			Bank = rom.Data[offset++];
			Map = rom.Data[offset++];
			Filler = new Word(rom, offset);
			
		}


		//por mirar
		public byte[] GetBytes()
		{
			return Serializar.GetBytes((long)LType).AddArray(Offset.BytesPointer, new byte[] { Bank, Map }, Filler.Data);
		}
	}

}
