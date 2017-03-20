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
	/// Description of Variable.
	/// </summary>
	public class Variable:Zona
	{

		public Variable(string nombre):base(nombre)
		{}
		public Variable(Enum nombre):base(nombre)
		{}
		public static int GetVariable(Variable variable,EdicionPokemon edicion,Compilacion compilacion)
		{
			if(!variable.Diccionario.ContainsKey(compilacion)||!variable.Diccionario[compilacion].ContainsKey(edicion))
				throw new   RomFaltaInvestigacionException();
			return variable.Diccionario[compilacion][edicion];
		}
	}
}
