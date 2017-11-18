/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 18/11/2017
 * Hora: 5:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of DesactivarAnimacionMTMO.
	/// </summary>
	public static class DesactivarAnimacionMTMO
	{
		static readonly byte[] Activado1;
		static readonly byte[] Activado2;
		static readonly byte[] Desactivado1;
		static readonly byte[] Desactivado2;
		
		public static readonly Creditos Creditos;
		
		public static readonly Variable VariableDesactivarMTMO1;
		public static readonly Variable VariableDesactivarMTMO2;
		
		public const string DESCRIPCION = "Permite activar/desactivar la animación que hay cunado enseñas a un pokémon un ataque";
		static DesactivarAnimacionMTMO()
		{
			Creditos = new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY], "Tara", "Investigación y post https://www.pokecommunity.com/showpost.php?p=8583054&postcount=30");
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO], "Lunos", "Hacer post https://wahackforo.com/t-51754/fr-otros-desactivar-animacion-que-aparece-al-usar-tm");
			Activado1 = new byte[]{ 0x0, 0x0 };
			Desactivado1 = new byte[]{ 0x0B, 0xD0 };
			
			Activado2 = new byte[]{ 0x00, 0xF0, 0x0E, 0xFA };
			Desactivado2 = new byte[]{ 0x5A, 0xF7, 0x78, 0xFE };//3Byte->8C verde10,3Byte->5A verde11,A partir3Byte->F4 FD RojoFuego
			
			VariableDesactivarMTMO1 = new Variable("Desactivar animación aprender MT/MO parte1");
			VariableDesactivarMTMO2 = new Variable("Desactivar animación aprender MT/MO parte2");
			
			//pongo las variables
			//offset1
			VariableDesactivarMTMO1.Add(EdicionPokemon.RojoFuegoUsa, 0x11CE6E, 0x11CEE6);
			VariableDesactivarMTMO1.Add(EdicionPokemon.VerdeHojaUsa, 0x11CE46, 0x11CEBE);
			VariableDesactivarMTMO1.Add(EdicionPokemon.RojoFuegoEsp, 0x11CFAE);
			VariableDesactivarMTMO1.Add(EdicionPokemon.VerdeHojaEsp, 0x11CF86);
			//offset2
			VariableDesactivarMTMO2.Add(EdicionPokemon.RojoFuegoUsa, 0x11CA2C, 0x11CAA4);
			VariableDesactivarMTMO2.Add(EdicionPokemon.VerdeHojaUsa, 0x11CA04, 0x11CA7C);
			VariableDesactivarMTMO2.Add(EdicionPokemon.RojoFuegoEsp, 0x11CB6C);
			VariableDesactivarMTMO2.Add(EdicionPokemon.VerdeHojaEsp, 0x11CB44);
			
			
			
		}
		public static bool Compatible(EdicionPokemon edicion, Compilacion compilacion)
		{
			bool compatible = VariableDesactivarMTMO1.Diccionario.ContainsKey(compilacion);
			if (compatible)
				compatible = VariableDesactivarMTMO1.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offset = Variable.GetVariable(VariableDesactivarMTMO1, edicion, compilacion);
			return rom.Data.Bytes[offset] == Activado1[0] && rom.Data.Bytes[offset + 1] == Activado1[1];
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom, rom.Edicion, rom.Compilacion);
		}
		public static void Activar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offset1 = Variable.GetVariable(VariableDesactivarMTMO1, edicion, compilacion);
			int offset2 = Variable.GetVariable(VariableDesactivarMTMO2, edicion, compilacion);
			rom.Data.SetArray(offset1, Activado1);
			rom.Data.SetArray(offset2, Activado2);
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
		}
		public static void Desactivar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offset1 = Variable.GetVariable(VariableDesactivarMTMO1, edicion, compilacion);
			int offset2 = Variable.GetVariable(VariableDesactivarMTMO2, edicion, compilacion);
			rom.Data.SetArray(offset1, Desactivado1);
			rom.Data.SetArray(offset2, Desactivado2);
			//3Byte->8C verde10,3Byte->5A verde11,A partir3Byte->F4 FD RojoFuego
			if (edicion.AbreviacionRom == AbreviacionCanon.BPR) {
				if (edicion.Idioma == Idioma.Español)
					rom.Data.SetArray(offset2 + 2, new byte[]{ 0xF4, 0xFD });
			} else {
			
				if (compilacion == Compilacion.Compilaciones[0])
					rom[offset2 + 2] = 0x8C;
				else
					rom[offset2 + 2] = 0x5A;
			}
		}
	}
}
