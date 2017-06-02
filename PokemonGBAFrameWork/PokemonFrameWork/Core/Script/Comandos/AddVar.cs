/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 3:12
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFrameWork.Script
{
	public class AddVar : SetVar
	{
		public const byte ID = 0x17;

		public AddVar(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public AddVar(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe AddVar(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		#region implemented abstract members of Comando
		public override string Descripcion {
			get {
				return "Añade cualquier valor a la variable";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "Addvar";
			}
		}
	#endregion
	}
}


