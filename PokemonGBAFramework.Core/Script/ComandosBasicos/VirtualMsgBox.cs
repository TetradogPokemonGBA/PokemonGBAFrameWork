/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualMsgBox.
	/// </summary>
	public class VirtualMsgBox:VirtualLoadPointer
	{
		public new const byte ID = 0xBD;
		public new const string NOMBRE = "VirtualMsgBox";
		public new const string DESCRIPCION = "Prepara un puntero para ser usado en una caja de texto.";


		public VirtualMsgBox() { }
		public VirtualMsgBox(BloqueString text):base(text)
		{
 
		}
   
		public VirtualMsgBox(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualMsgBox(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualMsgBox(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;

	}
}
