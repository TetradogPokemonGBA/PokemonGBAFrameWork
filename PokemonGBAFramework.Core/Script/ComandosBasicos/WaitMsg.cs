/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WaitMsg.
	/// </summary>
	public class WaitMsg:Comando
	{
		public const byte ID = 0x66;
		public const int SIZE = 1;
  
		public WaitMsg()
		{
   
		}
   
		public WaitMsg(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WaitMsg(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WaitMsg(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Espera a que preparemsg acabe";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WaitMsg";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
         
	}
}
