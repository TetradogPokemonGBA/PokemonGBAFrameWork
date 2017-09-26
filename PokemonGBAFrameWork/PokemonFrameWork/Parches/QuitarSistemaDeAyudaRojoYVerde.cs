/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 16/03/2017
 * Time: 20:50
 * 
 * Código bajo licencia GNU
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//funciona :D
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of QuitarSistemaDeAyudaRojoYVerde.
	/// </summary>
	public static class QuitarSistemaDeAyudaRojoYVerde
	{
		public const byte ACTIVADO=0xE0;
		public const byte DESACTIVADO=0xD1;
		public static readonly Variable VariableQuitarSistemaDeAyudaRojoYVerde;
		public static readonly Creditos Creditos;
		static QuitarSistemaDeAyudaRojoYVerde()
		{
			VariableQuitarSistemaDeAyudaRojoYVerde=new Variable("Variable quitar sistema de ayuda Rojo y Verde");
			VariableQuitarSistemaDeAyudaRojoYVerde.Add(EdicionPokemon.RojoFuegoEsp,0x13BA8B);
			VariableQuitarSistemaDeAyudaRojoYVerde.Add(EdicionPokemon.VerdeHojaEsp,0x13BA63);
			VariableQuitarSistemaDeAyudaRojoYVerde.Add(EdicionPokemon.RojoFuegoUsa,0x13B8C3,0x13B93B);
			VariableQuitarSistemaDeAyudaRojoYVerde.Add(EdicionPokemon.VerdeHojaUsa,0x13B89B,0x13B913);
			//Creditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"FraynSebas","Hacer la investigación");
			
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=VariableQuitarSistemaDeAyudaRojoYVerde.Diccionario.ContainsKey(compilacion);
			if(compatible)
				compatible=VariableQuitarSistemaDeAyudaRojoYVerde.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return rom.Data[Variable.GetVariable(VariableQuitarSistemaDeAyudaRojoYVerde,edicion,compilacion)]==ACTIVADO;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data[Variable.GetVariable(VariableQuitarSistemaDeAyudaRojoYVerde,edicion,compilacion)]=ACTIVADO;
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data[Variable.GetVariable(VariableQuitarSistemaDeAyudaRojoYVerde,edicion,compilacion)]=DESACTIVADO;
		}
	}
}
