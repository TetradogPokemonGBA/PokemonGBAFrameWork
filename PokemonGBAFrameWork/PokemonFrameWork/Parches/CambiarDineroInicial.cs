/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 16/03/2017
 * Time: 21:25
 * 
 * Código bajo licencia GNU
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//funciona bien :3
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of CambiarDineroInicial.
	/// </summary>
	public static class CambiarDineroInicial
	{
		public static readonly Creditos Creditos;
		public const int MAX=999999;//diria que sin hacks no se puede...por mirar
		public const int DEFAULT=3000;
		public static readonly Variable VariableCambiarDineroInicial;
		static CambiarDineroInicial()
		{
			VariableCambiarDineroInicial=new Variable("Variable Cambiar dinero inicial");
			VariableCambiarDineroInicial.Add(EdicionPokemon.RubiUsa,0x52F4C,0x52F6C);
			VariableCambiarDineroInicial.Add(EdicionPokemon.ZafiroUsa,0x52F4C,0x52F6C);
			VariableCambiarDineroInicial.Add(0x53388,EdicionPokemon.ZafiroEsp,EdicionPokemon.RubiEsp);
			VariableCambiarDineroInicial.Add(EdicionPokemon.RojoFuegoUsa,0x54B60,0x54B74);
			VariableCambiarDineroInicial.Add(EdicionPokemon.VerdeHojaUsa,0x54B60,0x54B74);
			VariableCambiarDineroInicial.Add(0x54C54,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			VariableCambiarDineroInicial.Add(EdicionPokemon.EsmeraldaUsa,0x845BC);
			VariableCambiarDineroInicial.Add(EdicionPokemon.EsmeraldaEsp,0x845D0);
			//Créditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"Sonicarvalho","Decir como se hace");
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=VariableCambiarDineroInicial.Diccionario.ContainsKey(compilacion);
			if(compatible)
				compatible=VariableCambiarDineroInicial.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static int GetDineroInicial(RomData rom)
		{
			return GetDineroInicial(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetDineroInicial(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return DWord.GetDWord(rom,Variable.GetVariable(VariableCambiarDineroInicial,edicion,compilacion));
		}
		public static void SetDineroInicial(RomData rom,int dineroIncial=DEFAULT)
		{
			SetDineroInicial(rom.Rom,rom.Edicion,rom.Compilacion,dineroIncial);
		}
		public static void SetDineroInicial(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int dineroInicial=DEFAULT)
		{
			DWord.SetDWord(rom,Variable.GetVariable(VariableCambiarDineroInicial,edicion,compilacion),dineroInicial);
		}
	}
}
