/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 11:51
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Esta clase se usa para guardar offsets u otros datos necesarios separados por ediciones y compilaciones
	/// </summary>
	public class Variable:Var
	{

		public Variable(string nombre):base(nombre)
		{}
		public Variable(Enum nombre):base(nombre)
		{}
		public static int GetVariable(Variable variable,EdicionPokemon edicion,Compilacion compilacion)
		{ 
		 	return GetValue(variable,edicion,compilacion);
		}
	}
}
