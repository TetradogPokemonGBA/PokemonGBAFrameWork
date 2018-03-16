/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 15/07/2017
 * Hora: 16:27
 * 
 * Código bajo licencia GNU
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;


namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of QuitarDiarioPartida.
	/// </summary>
	public static class QuitarDiarioPartida
	{
		public static readonly Creditos Creditos;
		public static readonly byte[] Desactivado1={0x01,0x30};
		public static readonly byte[] Desactivado2={0xEF,0xD9};
		
		public static readonly byte[] Activado1={0x00,0x20};
		public static readonly byte[] Activado2={0xC0,0x46};
		
		public static readonly Variable VariableOffset1;
		public static readonly Variable VariableOffset2;
		public const string DESCRIPCION="Permite quitar el dinario de la partida (el hagamos memoria)";
		
		static QuitarDiarioPartida()
		{
			VariableOffset1=new Variable("Quitar diario offset1");
			VariableOffset2=new Variable("Quitar diario offset2");
			
			//Offset1
			VariableOffset1.Add(EdicionPokemon.RojoFuegoUsa,0x110F44,0x110FBC);
			VariableOffset1.Add(EdicionPokemon.VerdeHojaUsa,0x110F1C,0x110F94);
			
			VariableOffset1.Add(EdicionPokemon.RojoFuegoEsp,0x111120);
			VariableOffset1.Add(EdicionPokemon.VerdeHojaEsp,0x1110F8);
			
			
			//Offset2
			VariableOffset2.Add(EdicionPokemon.RojoFuegoUsa,0x110F50,0x110FC8);
			VariableOffset2.Add(EdicionPokemon.VerdeHojaUsa,0x110F28,0x110FA0);
			
			VariableOffset2.Add(EdicionPokemon.RojoFuegoEsp,0x11112C);
			VariableOffset2.Add(EdicionPokemon.VerdeHojaEsp,0x111104);
			//Creditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"Darthatron","hacer investigación https://wahackforo.com/t-37039/fr-otros-eliminar-diario-previously-on-your-quest");
			
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=VariableOffset1.Diccionario.ContainsKey(compilacion);
			if(compatible)
				compatible=VariableOffset1.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return rom.Data.Bytes.ArrayEqual(Activado1,Variable.GetVariable(VariableOffset1,edicion,compilacion))&&rom.Data.Bytes.ArrayEqual(Activado2,Variable.GetVariable(VariableOffset2,edicion,compilacion));
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data.SetArray(Variable.GetVariable(VariableOffset1,edicion,compilacion),Activado1);
			rom.Data.SetArray(Variable.GetVariable(VariableOffset2,edicion,compilacion),Activado2);
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			rom.Data.SetArray(Variable.GetVariable(VariableOffset1,edicion,compilacion),Desactivado1);
			rom.Data.SetArray(Variable.GetVariable(VariableOffset2,edicion,compilacion),Desactivado2);
		}
	}
}
