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
		public const string NOMBRE = "WaitKeyPress";
		public const string DESCRIPCION = "Espera hasta que se pulsa una tecla";


		public WaitKeyPress()
		{
   
		}
   
		public WaitKeyPress(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WaitKeyPress(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WaitKeyPress(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;



	}
}
