/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WaitKeyPress.
	/// </summary>
	public class WaitKeyPress:Comando
	{
		public const byte ID = 0x6D;
		public const int SIZE = 1;
  
		public WaitKeyPress()
		{
   
		}
   
		public WaitKeyPress(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public WaitKeyPress(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe WaitKeyPress(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Espera hasta que se pulsa una tecla";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WaitKeyPress";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
                         
		
	}
}
