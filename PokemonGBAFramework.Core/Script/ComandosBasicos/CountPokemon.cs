﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:36
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CountPokemon.
	/// </summary>
	public class CountPokemon:Comando
	{
		public const byte ID = 0x43;
        public const string NOMBRE= "CountPokemon";
        public const string DESCRIPCION= "Cuenta el numero de pokemon del equipo y guarda el resultado en 'lastresult'";

        public CountPokemon()
		{}
		public CountPokemon(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public CountPokemon(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe CountPokemon(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
                return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		
	}
}