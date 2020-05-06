﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:03
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class CheckFlag : SetFlag
	{
		public new const byte ID = 0x2B;
		public const string NOMBRE="CheckFlag";
		public const string DESCRIPCION="Comprueba el estado del flag y lo guarda en 'lastresult'";

		public CheckFlag(Word flag):base(flag)
		{}
		public CheckFlag(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public CheckFlag(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe CheckFlag(byte* ptRom, int offset) : base(ptRom, offset)
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


