/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PrepareMsg3.
	/// </summary>
	public class PrepareMsg3:PrepareMsg
	{
		public new const byte ID = 0xDB;
		public new const string NOMBRE = "PrepareMsg3";
		public new const string DESCRIPCION = "Bajo investigación...";

		public PrepareMsg3() { }
		public PrepareMsg3(BloqueString texto):base(texto)
		{
		
 
		}
   
		public PrepareMsg3(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public PrepareMsg3(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe PrepareMsg3(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;

	}
}
