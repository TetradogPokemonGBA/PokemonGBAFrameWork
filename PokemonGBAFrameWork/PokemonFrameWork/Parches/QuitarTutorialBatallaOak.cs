/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 19/05/2017
 * Hora: 20:24
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of QuitarTutorialBatallaOak.
	/// </summary>
	public static class QuitarTutorialBatallaOak
	{
		public static readonly Creditos Creditos;
		public static readonly Variable VariableQuitarTutorialBatallaOak;
		static readonly byte[] Off={0x20, 0x68, 0x10, 0x21, 0x08, 0x43 ,0x20};
		const byte ON=0x0;
		const int LENGTH = 7;
		
		static QuitarTutorialBatallaOak()
		{
			VariableQuitarTutorialBatallaOak=new Variable("QuitarTutorialBatallaOak");
			VariableQuitarTutorialBatallaOak.Add(EdicionPokemon.RojoFuegoUsa,0x80484,0x80498);
			VariableQuitarTutorialBatallaOak.Add(EdicionPokemon.VerdeHojaUsa,0x80458,0x8046C);
			VariableQuitarTutorialBatallaOak.Add(EdicionPokemon.RojoFuegoEsp,0x804B4);
			VariableQuitarTutorialBatallaOak.Add(EdicionPokemon.VerdeHojaEsp,0x80488);
			//Creditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"Knizz","sacado de https://wahackforo.com/t-41133/miniaporte-como-quitar-mensaje-oak-en-batalla");
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=VariableQuitarTutorialBatallaOak.Diccionario.ContainsKey(compilacion);
			if(compatible)
				compatible=VariableQuitarTutorialBatallaOak.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int offset=Variable.GetVariable(VariableQuitarTutorialBatallaOak,edicion,compilacion);
			return rom.Data.Bytes[offset]==ON;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int offset=Variable.GetVariable(VariableQuitarTutorialBatallaOak,edicion,compilacion);
			rom.Data.Remove(offset,LENGTH,ON);
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int offset=Variable.GetVariable(VariableQuitarTutorialBatallaOak,edicion,compilacion);
			rom.Data.SetArray(offset,Off);
		}
	}
}
