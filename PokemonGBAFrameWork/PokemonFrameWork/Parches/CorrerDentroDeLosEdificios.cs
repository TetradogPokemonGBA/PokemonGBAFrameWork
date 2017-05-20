/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 16/03/2017
 * Time: 20:06
 * 
 * Código bajo licencia GNU
 * créditos:Guilly Alpha de Wahack
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//funciona :D
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of CorrerDentroDeLosEdificios.
	/// </summary>
	public static class CorrerDentroDeLosEdificios
	{
		const byte ACTIVADO=0x0;
		const byte DESACTIVADO=0x8;
		public static readonly Variable VariableCorrerDentroDeLosEdificios;
		static CorrerDentroDeLosEdificios()
		{
			VariableCorrerDentroDeLosEdificios=new Variable("Variable offset correr dentro de los edificios");
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.RojoFuegoEsp,0xBD648);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.VerdeHojaEsp,0xBD61C);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.RojoFuegoUsa,0xBD494,0xBD4A8);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.VerdeHojaUsa,0xBD468,0xBD47C);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.RubiEsp,0xE620C);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.ZafiroEsp,0xE620C);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.RubiUsa,0xE5E00,0xE5E20);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.ZafiroUsa,0xE5E00,0xE5E20);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.EsmeraldaEsp,0x119E00);
			VariableCorrerDentroDeLosEdificios.Add(EdicionPokemon.EsmeraldaUsa,0x11A1E8);
			
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return rom.Data[Variable.GetVariable(VariableCorrerDentroDeLosEdificios,edicion,compilacion)]==ACTIVADO;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data[Variable.GetVariable(VariableCorrerDentroDeLosEdificios,edicion,compilacion)]=ACTIVADO;
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data[Variable.GetVariable(VariableCorrerDentroDeLosEdificios,edicion,compilacion)]=DESACTIVADO;
		}
		
	}
}
