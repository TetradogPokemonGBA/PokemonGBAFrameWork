/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 29/05/2017
 * Hora: 6:43
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of PokemonSalvaje.
	/// </summary>
	public class PokemonSalvaje
	{
		public static readonly Creditos Creditos;
		public const int LENGHT=4;
		Pokemon pokemon;
		byte nivelMinimo;
		byte nivelMaximo;
		static PokemonSalvaje()
		{
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.GITHUB],"shinyQuagsire","por hacer MEH");
		}
		public PokemonSalvaje()
		{
		}
		public PokemonSalvaje(Pokemon pokemon,int nivelMinimo,int nivelMaximo)
		{
			Pokemon=pokemon;
			NivelMinimo=(byte)nivelMinimo;
			NivelMaximo=(byte)nivelMaximo;
		}
		
		//será el orden nacional?
		public Pokemon Pokemon {
			get {
				return pokemon;
			}
			set {
				pokemon = value;
			}
		}

		public byte NivelMinimo {
			get {
				return nivelMinimo;
			}
			set {
				nivelMinimo = value;
			}
		}

		public byte NivelMaximo {
			get {
				return nivelMaximo;
			}
			set {
				nivelMaximo = value;
			}
		}
		public static PokemonSalvaje GetPokemonSalvaje(RomGba rom,IList<Pokemon> pokedex,int offsetPokemon)
		{
			PokemonSalvaje pokemon=new PokemonSalvaje();
			Pokemon.OrdenPokemon orden=Pokemon.Orden;
			Pokemon.Orden=Pokemon.OrdenPokemon.Nacional;//supongo que es este pero no lo se...por mirar...
			pokedex=pokedex.SortByQuickSort();
			Pokemon.Orden=orden;
			pokemon.NivelMinimo=rom.Data[offsetPokemon++];
			pokemon.NivelMaximo=rom.Data[offsetPokemon++];
			pokemon.Pokemon=pokedex[Word.GetWord(rom,offsetPokemon)];
			return pokemon;
		}
		public static void SetPokemonSalvaje(RomGba rom,int offsetPokemon,PokemonSalvaje pokemonSalvaje)
		{
			rom.Data[offsetPokemon++]=pokemonSalvaje.NivelMinimo;
			rom.Data[offsetPokemon++]=pokemonSalvaje.NivelMaximo;
			Word.SetWord(rom,offsetPokemon,(short)pokemonSalvaje.Pokemon.OrdenNacional);
		}
	}
}
