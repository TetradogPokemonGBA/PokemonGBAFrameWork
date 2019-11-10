﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:02
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFrameWork.ComandosScript
{
	public class Jumpram : Comando
	{
		public const byte ID = 0xC;
		public new const int SIZE = 1;
        public const string NOMBRE= "Jumpram";
        public const string DESCRIPCION= "Salta a la dirección por defecto de la memoria ram y ejecuta el script guardado allí";

        public Jumpram()
		{}
		public Jumpram(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Jumpram(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Jumpram(byte* ptRom, int offset) : base(ptRom, offset)
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

		#region implemented abstract members of Comando
		public override int Size {
			get {
				return SIZE;
			}
		}
	#endregion
	}
}


