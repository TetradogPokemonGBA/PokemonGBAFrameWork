/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:53
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	public static class Shinyzer
	{
		public static readonly ASM Rutina=ASM.Compilar(Resources.ASMShinyzer);
		public static string VariableShinytzer = "0x8003";

		public static bool EstaActivado(RomGba rom)
		{
			return PosicionRutinaShinyzer(rom) > 0;
		}
		public static int Activar(RomData rom)
		{
			return Activar(rom.Rom,rom.Edicion);
		}
		public static int Activar(RomGba rom,EdicionPokemon edicion)
		{
			int posicion = PosicionRutinaShinyzer(rom);
			if(posicion<0)
				posicion=rom.Data.SetArray(Rutina.AsmBinary);
			return posicion;

		}
		public static void Desactivar(RomGba rom)
		{
			int posicion = PosicionRutinaShinyzer(rom);
			if (posicion > 0)
			{
				rom.Data.Remove( posicion, Rutina.AsmBinary.Length);
			}
		}
		/// <summary>
		/// Permite saber donde esta la rutina insertada
		/// </summary>
		/// <param name="rom"></param>
		/// <returns>devuelve -1 si no lo ha encontrado</returns>
		public  static int PosicionRutinaShinyzer(RomGba rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			return rom.Data.SearchArray(Rutina.AsmBinary);
		}
		public static string ScriptLineaPokemonShiny(short numPokemon)
		{
			if (numPokemon<0)
				throw new ArgumentOutOfRangeException();

			return "setvar " + VariableShinytzer + ((Hex)numPokemon).ByteString;
		}
		/// <summary>
		/// Crear fácilmente el script shinytzer para que el entrenador al ser llamado tenga esos pokemon
		/// </summary>
		/// <param name="entrenador"></param>
		/// <param name="pokemon">primer,segundo,tercero,cuarto...</param>
		/// <returns></returns>
		public static string ScriptLineaPokemonShinyEntrenador(Entrenador entrenador,params bool[] pokemon)
		{
			if (entrenador == null)
				throw new ArgumentNullException();

			const int MAXPOKEMONENTRENADOR = 6,BITSBYTE=8;

			Hex numPokemonShiny;
			bool[] pokemonFinal;

			if (pokemon.Length < MAXPOKEMONENTRENADOR)
			{
				pokemonFinal = new bool[MAXPOKEMONENTRENADOR];
				for (int i =0; i< pokemon.Length;  i++)
					pokemonFinal[i] = pokemon[i];
				
			}
			else if (pokemon.Length > MAXPOKEMONENTRENADOR)
				pokemonFinal = pokemon.SubArray(0, MAXPOKEMONENTRENADOR);
			else pokemonFinal = pokemon;

			pokemon = pokemonFinal;
			pokemonFinal = new bool[BITSBYTE];
			for (int i = 2,j=0; j<pokemon.Length; i++,j++)
				pokemonFinal[i] = pokemon[j];

			numPokemonShiny = pokemonFinal.ToByte();
			
			return "setvar " + VariableShinytzer + " 0x" + numPokemonShiny.ToString().PadLeft(2, '0') + entrenador.EquipoPokemon.NumeroPokemon.ToString().PadLeft(2,'0');
		}
		public static string SimpleScriptBattleShinyTrainer(int indexEntrenador,Entrenador  entrenador,params bool[] pokemon)
		{
			StringBuilder strScript=new StringBuilder();
			strScript.Append("#dynamic 0x800000 \r\n#org @ScriptTrainer");
			strScript.Append(entrenador.Nombre);
			strScript.Append("Shiny\r\nlock\r\nfaceplayer\r\n");
			strScript.Append(ScriptLineaPokemonShinyEntrenador(entrenador,pokemon));
			strScript.Append("\r\ntrainerbattle 0x0 ");
			strScript.Append(((Hex)(indexEntrenador+1)).ByteString);
			strScript.Append(" 0x0 0x0 0x0\r\nrelease\r\nend");
			return strScript.ToString();
		}
	}
}
