/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PrepareMsg2.
	/// </summary>
	public class PrepareMsg2:PrepareMsg
	{
		public new const byte ID = 0x9B;
		public new const string NOMBRE= "PrepareMsg2";
		public new const string DESCRIPCION= "Bajo investigación...";

		public PrepareMsg2() { }
		public PrepareMsg2(BloqueString pointerTexto):base(pointerTexto)
		{

 
		}
   
		public PrepareMsg2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public PrepareMsg2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe PrepareMsg2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;

	}
}
