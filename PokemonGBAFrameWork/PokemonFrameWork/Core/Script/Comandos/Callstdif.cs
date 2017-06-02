/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 1:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFrameWork
{
	public class Callstdif : Gotostdif
	{
		public const byte ID = 0xB;

		public const int SIZE = 3;

		public Callstdif(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Callstdif(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Callstdif(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "Callstdif";
			}
		}

		public override string Descripcion {
			get {
				return "llama a la función si se cumple la condición";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
	}
}


