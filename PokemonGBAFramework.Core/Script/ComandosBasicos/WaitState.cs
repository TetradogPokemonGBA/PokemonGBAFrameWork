﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:58
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WaitState.
	/// </summary>
	public class WaitState:Comando
	{
		public const byte ID = 0x27;
	    public const string DESCRIPCION= "Pone a esperar al script a que cambie el estado del special o comando";
        public const string NOMBRE= "WaitState";
        public WaitState()
		{
		}
		public WaitState(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public WaitState(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe WaitState(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
                return NOMBRE;
			}
		}

	}
}