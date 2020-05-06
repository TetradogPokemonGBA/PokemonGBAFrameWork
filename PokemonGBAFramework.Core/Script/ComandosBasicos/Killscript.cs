﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:02
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class Killscript : Comando
	{
		public const byte ID = 0xD;
        public const string NOMBRE= "Killscript";
        public const string DESCRIPCION= "Acaba con el script y restaura la ram";

        public Killscript()
		{}

		public Killscript(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Killscript(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Killscript(byte* ptRom, int offset) : base(ptRom, offset)
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


