/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CmdB1.
	/// </summary>
	public class CmdB1:Comando
	{
		public const byte ID = 0xB1;

        public const string NOMBRE = "CmdB1";
        public CmdB1()
		{

 
		}
   
		public CmdB1(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CmdB1(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CmdB1(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Bajo investigación,podria hacer igual que nop";
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
