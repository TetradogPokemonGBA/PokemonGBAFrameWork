/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 15/07/2017
 * Hora: 14:54
 * 
 * Código bajo licencia GNU
 *  creditos al autor del descubrimiento JPAN de pokemoncommunity, sacado del post https://wahackforo.com/t-49334/fr-permitir-que-tus-pokemon-puedan-olvidar-cualquier-ataque
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
		
		public static readonly Zona ZonaOffset1;
		public static readonly Zona ZonaOffset2;
		
		static BorrarMos()
		{
			ZonaOffset1=new Zona("Borrar MOs offset1");
			ZonaOffset2=new Zona("Borrar MOs offset2");
			
			//Offset1
			ZonaOffset1.Add(EdicionPokemon.RojoFuegoUsa,0x441D6,0x441EA);
			ZonaOffset1.Add(EdicionPokemon.VerdeHojaUsa,0x441D6,0x441EA);
			
			ZonaOffset1.Add(EdicionPokemon.RojoFuegoEsp,0x440C2);
			ZonaOffset1.Add(EdicionPokemon.VerdeHojaEsp,0x440C2);
			
			ZonaOffset1.Add(EdicionPokemon.EsmeraldaUsa,0x6E822);
			ZonaOffset1.Add(EdicionPokemon.EsmeraldaEsp,0x6E822);
			
			ZonaOffset1.Add(EdicionPokemon.RubiEsp,0x40C0A);
			ZonaOffset1.Add(EdicionPokemon.ZafiroEsp,0x40C0A);
			
			ZonaOffset1.Add(EdicionPokemon.RubiUsa,0x40A1E,0x40AA4,0x40A3E);
			ZonaOffset1.Add(EdicionPokemon.ZafiroUsa,0x40A1E,0x40A3E,0x40A3E);
			
			//Offset2
			ZonaOffset2.Add(EdicionPokemon.RojoFuegoUsa,0x125AA8,0x125B20);
			ZonaOffset2.Add(EdicionPokemon.VerdeHojaUsa,0x125A80,0x125AF8);
			
			ZonaOffset2.Add(EdicionPokemon.RojoFuegoEsp,0x125C24);
			ZonaOffset2.Add(EdicionPokemon.VerdeHojaEsp,0x125BFC);
			
			ZonaOffset2.Add(EdicionPokemon.EsmeraldaUsa,0x1B6D2C);
			ZonaOffset2.Add(EdicionPokemon.EsmeraldaEsp,0x1B694C);
			
			ZonaOffset2.Add(EdicionPokemon.RubiEsp,0x6F490);
			ZonaOffset2.Add(EdicionPokemon.ZafiroEsp,0x6F494);
			
			ZonaOffset2.Add(EdicionPokemon.RubiUsa,0x6F054,0x6F074,0x6F074);
			ZonaOffset2.Add(EdicionPokemon.ZafiroUsa,0x6F058,0x6F078,0x6F078);

		}
		
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static bool EstaActivado(RomGba romGBA, EdicionPokemon edicion,Compilacion compilacion)
		{
			return romGBA.Data[Zona.GetOffsetRom(romGBA, ZonaOffset1, edicion, compilacion)]==ON&&romGBA.Data[Zona.GetOffsetRom(romGBA, ZonaOffset2, edicion, compilacion)]==ON;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static void Activar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
		{
			romGBA.Data[Zona.GetOffsetRom(romGBA, ZonaOffset1, edicion, compilacion)]=ON;
			romGBA.Data[Zona.GetOffsetRom(romGBA, ZonaOffset2, edicion, compilacion)]=ON;
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static void Desactivar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
		{
			romGBA.Data[Zona.GetOffsetRom(romGBA, ZonaOffset1, edicion, compilacion)]=OFF;
			romGBA.Data[Zona.GetOffsetRom(romGBA, ZonaOffset2, edicion, compilacion)]=OFF;
		}
		
	}
}
