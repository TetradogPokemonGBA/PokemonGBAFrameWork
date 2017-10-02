/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 15/07/2017
 * Hora: 14:54
 * 
 * Código bajo licencia GNU
 *  creditos al autor del descubrimiento  de pokemoncommunity, 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of BorrarMos.
	/// </summary>
	public static class BorrarMos
	{
		const byte ON=0x00;
		const byte OFF=0x01;
		public static readonly Creditos Creditos;
		public static readonly Variable VariableOffset1;
		public static readonly Variable VariableOffset2;
		public const string DESCRIPCION="Permite borrar una MO cuando se necesite como si fuera una ataque más.";
		static BorrarMos()
		{
			VariableOffset1=new Variable("Borrar MOs offset1");
			VariableOffset2=new Variable("Borrar MOs offset2");
			
			//Offset1
			VariableOffset1.Add(EdicionPokemon.RojoFuegoUsa,0x441D6,0x441EA);
			VariableOffset1.Add(EdicionPokemon.VerdeHojaUsa,0x441D6,0x441EA);
			
			VariableOffset1.Add(EdicionPokemon.RojoFuegoEsp,0x440C2);
			VariableOffset1.Add(EdicionPokemon.VerdeHojaEsp,0x440C2);
			
			VariableOffset1.Add(EdicionPokemon.EsmeraldaUsa,0x6E822);
			VariableOffset1.Add(EdicionPokemon.EsmeraldaEsp,0x6E822);
			
			VariableOffset1.Add(EdicionPokemon.RubiEsp,0x40C0A);
			VariableOffset1.Add(EdicionPokemon.ZafiroEsp,0x40C0A);
			
			VariableOffset1.Add(EdicionPokemon.RubiUsa,0x40A1E,0x40AA4,0x40A3E);
			VariableOffset1.Add(EdicionPokemon.ZafiroUsa,0x40A1E,0x40A3E,0x40A3E);
			
			//Offset2
			VariableOffset2.Add(EdicionPokemon.RojoFuegoUsa,0x125AA8,0x125B20);
			VariableOffset2.Add(EdicionPokemon.VerdeHojaUsa,0x125A80,0x125AF8);
			
			VariableOffset2.Add(EdicionPokemon.RojoFuegoEsp,0x125C24);
			VariableOffset2.Add(EdicionPokemon.VerdeHojaEsp,0x125BFC);
			
			VariableOffset2.Add(EdicionPokemon.EsmeraldaUsa,0x1B6D2C);
			VariableOffset2.Add(EdicionPokemon.EsmeraldaEsp,0x1B694C);
			
			VariableOffset2.Add(EdicionPokemon.RubiEsp,0x6F490);
			VariableOffset2.Add(EdicionPokemon.ZafiroEsp,0x6F494);
			
			VariableOffset2.Add(EdicionPokemon.RubiUsa,0x6F054,0x6F074,0x6F074);
			VariableOffset2.Add(EdicionPokemon.ZafiroUsa,0x6F058,0x6F078,0x6F078);
			//Créditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY],"JPAN","sacado del post https://wahackforo.com/t-49334/fr-permitir-que-tus-pokemon-puedan-olvidar-cualquier-ataque");

		}
		
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
					bool compatible=VariableOffset1.Diccionario.ContainsKey(compilacion);
			if(compatible)
				compatible=VariableOffset1.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomGba romGBA, EdicionPokemon edicion,Compilacion compilacion)
		{
			return romGBA.Data[Variable.GetVariable(VariableOffset1, edicion, compilacion)]==ON&&romGBA.Data[Variable.GetVariable( VariableOffset2, edicion, compilacion)]==ON;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static void Activar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
		{
			romGBA.Data[Variable.GetVariable( VariableOffset1, edicion, compilacion)]=ON;
			romGBA.Data[Variable.GetVariable( VariableOffset2, edicion, compilacion)]=ON;
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static void Desactivar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
		{
			romGBA.Data[Variable.GetVariable( VariableOffset1, edicion, compilacion)]=OFF;
			romGBA.Data[Variable.GetVariable( VariableOffset2, edicion, compilacion)]=OFF;
		}
		
	}
}
