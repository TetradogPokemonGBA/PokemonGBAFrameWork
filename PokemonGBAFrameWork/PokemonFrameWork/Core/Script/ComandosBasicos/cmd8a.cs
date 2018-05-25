/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//corregido http://www.sphericalice.com/romhacking/documents/script/index.html#c-77
namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of cmd8a.
	/// </summary>
	public class Cmd8A:Comando
	{
		public const byte ID=0x8A;

		public Cmd8A()
		{

			
		}
		
		public Cmd8A(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Cmd8A(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Cmd8A(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "[aparentemente no hace nada]";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Cmd8A";
			}
		}
	
	}
}
