/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//corregido http://www.sphericalice.com/romhacking/documents/script/index.html#c-77
namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Cmd96.
	/// </summary>
	public class Cmd96:Comando
	{
		public const byte ID = 0x96;
        public const string NOMBRE = "Cmd96";
        public const string DESCRIPCION= "Aparentemente no hace absolutamente nada al igual que nop";

        public Cmd96()
		{
 
		}
   
		public Cmd96(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Cmd96(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Cmd96(byte* ptRom, int offset)
			: base(ptRom, offset)
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
