/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 16/03/2017
 * Time: 20:59
 * 
 * Código bajo licencia GNU
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of ScriptsDeGatilloCondicionMayorAFF.
	/// </summary>
	public static class ScriptsDeGatilloCondicionMayorAFF
	{
		public static readonly byte[] Activado={0x21,0x89};
		public static readonly byte[] Desactivado={0x21,0x7A};
		public static readonly Variable VariableScriptsDeGatilloCondicionMayorAFF;
		public static readonly Creditos Creditos;
		public const string DESCRIPCION="Las variables pueden tener valores superiores a FF en los scripts de gatillo.";
		static ScriptsDeGatilloCondicionMayorAFF()
		{
			VariableScriptsDeGatilloCondicionMayorAFF=new Variable("Variable scripts de gatillo con condicion mayor que FF");
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.RojoFuegoEsp,0x6DDDE);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.VerdeHojaEsp,0x6DDDE);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.RojoFuegoUsa,0x6DDA6,0x6DDBA);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.VerdeHojaUsa,0x6DDA6,0x6DDBA);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.RubiEsp,0x691DE);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.ZafiroEsp,0x691E2);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.RubiUsa,0x68DA2,0x68DC2);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.ZafiroUsa,0x68DA6,0x68DC6);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.EsmeraldaEsp,0x9D086);
			VariableScriptsDeGatilloCondicionMayorAFF.Add(EdicionPokemon.EsmeraldaUsa,0x9D072);
			//Creditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"♠Ϛﮠც㆚ꂅℛᎧ♠"," por el post");
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=VariableScriptsDeGatilloCondicionMayorAFF.Diccionario.ContainsKey(compilacion);
			if(compatible)
				compatible=VariableScriptsDeGatilloCondicionMayorAFF.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return rom.Data.Bytes.ArrayEqual(Activado,Variable.GetVariable(VariableScriptsDeGatilloCondicionMayorAFF,edicion,compilacion));
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data.SetArray(Variable.GetVariable(VariableScriptsDeGatilloCondicionMayorAFF,edicion,compilacion),Activado);
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data.SetArray(Variable.GetVariable(VariableScriptsDeGatilloCondicionMayorAFF,edicion,compilacion),Desactivado);
		}
	}
}
