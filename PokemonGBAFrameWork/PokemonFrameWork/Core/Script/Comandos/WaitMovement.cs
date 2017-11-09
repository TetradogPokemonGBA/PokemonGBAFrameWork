/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of WaitMovement.
	/// </summary>
	public class WaitMovement:Comando
	{
		public const byte ID = 0x51;
		public const int SIZE = 3;
  
		public WaitMovement()
		{
   
		}
   
		public WaitMovement(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public WaitMovement(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe WaitMovement(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Espera a que el ApplyMovement acabe";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WaitMovement";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
                         
	
	}
}
