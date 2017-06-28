/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFrameWork.ComandosScript
{
	public class Goto : Call
	{
		public const byte ID = 0x5;

		public Goto(Script script):base(script)
		{}
		public Goto(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Goto(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Goto(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "Goto";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Continua con otro script";
			}
		}
	}
}


