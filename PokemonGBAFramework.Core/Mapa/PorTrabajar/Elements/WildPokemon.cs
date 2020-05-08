using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class WildPokemon
	{
		public const int LENGTH = 4;

		public WildPokemon(RomGba rom, int offset)
		{

			MinLV = rom.Data[offset++];
			MaxLV = rom.Data[offset++];
			Especie = new Word(rom, offset);
		}

		public WildPokemon(Word pokemon, int minLV=1, int maxLV=100)
        {

			MinLV = (byte)minLV;
			MaxLV = (byte)maxLV;
			Especie = pokemon;
		}

		public byte MinLV { get; set; }
		public byte MaxLV { get; set; }
		public Word Especie { get; set; }

		public byte[] GetBytes()
		{
			return new byte[] { MinLV, MaxLV }.AddArray(Especie!=default?Especie.Data:new byte[Word.LENGTH]);
		}
	}

}
