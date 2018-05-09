/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of WaitCry.
	/// </summary>
	public class WaitCry:Comando
	{
		public const byte ID = 0xC5;
		public const int SIZE = 1;
  
		public WaitCry()
		{
   
		}
   
		public WaitCry(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public WaitCry(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe WaitCry(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Espera a que acabe la ejecución de Cry.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WaitCry";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

	}
}
