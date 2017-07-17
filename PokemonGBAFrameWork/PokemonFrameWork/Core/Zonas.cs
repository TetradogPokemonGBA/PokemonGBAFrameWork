/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 11:11
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Se usa para guardar los offsets de los pointers que estan en las rutinas.
	/// </summary>
	public class Zona:Var
	{

		public Zona(string nombre):base(nombre)
		{}
		public Zona(Enum nombre):base(nombre)
		{}

		public static OffsetRom GetOffsetRom(RomGba rom, Zona zona, EdicionPokemon edicionPokemon, Compilacion compilacion)
		{

			return  new OffsetRom(rom,GetValue(zona,edicionPokemon,compilacion));
		}
	}
}
