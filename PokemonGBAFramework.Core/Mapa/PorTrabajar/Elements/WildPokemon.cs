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

			NivelMinimo = rom.Data[offset++];
			NivelMaximo = rom.Data[offset++];
			Especie = new Word(rom, offset);
		}

		public WildPokemon(Word pokemon, int minLV=1, int maxLV=0)
        {

			NivelMinimo = (byte)minLV;
			NivelMaximo = (byte)maxLV;
			Especie = pokemon;
		}

		public byte NivelMinimo { get; set; }
		public byte NivelMaximo { get; set; }
		public Word Especie { get; set; }

		public byte[] GetBytes()
		{
			return new byte[] { NivelMinimo, NivelMaximo }.AddArray(Especie!=default?Especie.Data:new byte[Word.LENGTH]);
		}
	}

}
