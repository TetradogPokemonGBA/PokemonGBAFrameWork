/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 16/03/2017
 * Time: 20:37
 * 
 * Código bajo licencia GNU
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat.Extension;
//fata hacerlo universal
namespace PokemonGBAFrameWork
{
	
	/// <summary>
	/// Description of EvolucionarSinNationalDex.
	/// </summary>
	public static class EvolucionarSinNationalDex
	{
		public static readonly Creditos Creditos;
		public static readonly byte[] Desactivado={0x97,0x28,0x14,0xDD};
		public static readonly byte[] Activado={0x0,0x0,0x14,0xE0};
		public static readonly Variable VariableEvolucionarSinNationalDex;
		public const string DESCRIPCION="Permite evolucionar los pokemon a evoluciones que estan fuera de la pokedex local, pj eevee->Espeon sin necesidad de tener la pokedex nacional";
		static EvolucionarSinNationalDex()
		{
			VariableEvolucionarSinNationalDex=new Variable("Variable evolucionar sin national Dex");
			VariableEvolucionarSinNationalDex.Add(EdicionPokemon.RojoFuegoEsp,0xCEB82);
			VariableEvolucionarSinNationalDex.Add(EdicionPokemon.VerdeHojaEsp,0xCEB56);
			VariableEvolucionarSinNationalDex.Add(EdicionPokemon.RojoFuegoUsa,0xCE91A,0xCE92E);
			VariableEvolucionarSinNationalDex.Add(EdicionPokemon.VerdeHojaUsa,0xCE8EE,0xCE902);
			//Creditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"♠Ϛﮠც㆚ꂅℛᎧ♠","Decir como se hace");
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=VariableEvolucionarSinNationalDex.Diccionario.ContainsKey(compilacion);
			if(compatible)
				compatible=VariableEvolucionarSinNationalDex.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return rom.Data.Bytes.ArrayEqual(Activado,Variable.GetVariable(VariableEvolucionarSinNationalDex,edicion,compilacion));
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data.SetArray(Variable.GetVariable(VariableEvolucionarSinNationalDex,edicion,compilacion),Activado);
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data.SetArray(Variable.GetVariable(VariableEvolucionarSinNationalDex,edicion,compilacion),Desactivado);
		}
	}
}
