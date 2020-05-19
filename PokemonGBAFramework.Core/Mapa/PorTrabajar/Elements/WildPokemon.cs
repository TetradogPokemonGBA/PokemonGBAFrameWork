using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class WildPokemon
	{
		public const int LENGTH = 1+1+Word.LENGTH;


		public WildPokemon():this(new Word(0)) { }
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
			return new byte[] { NivelMinimo, NivelMaximo }.AddArray(!Equals(Especie,default)?Especie.Data:new byte[Word.LENGTH]);
		}
		public static WildPokemon Get(ScriptAndASMManager scriptManager,RomGba rom, int offset)
		{
			WildPokemon wildPokemon = new WildPokemon();
			wildPokemon.NivelMinimo = rom.Data[offset++];
			wildPokemon.NivelMaximo = rom.Data[offset++];
			wildPokemon.Especie = new Word(rom, offset);
			return wildPokemon;
		}
	}

}
