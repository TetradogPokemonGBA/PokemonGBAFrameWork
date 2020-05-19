/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WaitCry.
	/// </summary>
	public class WaitCry:Comando
	{
		public const byte ID = 0xC5;
		public const int SIZE = 1;
  
		public WaitCry()
		{
   
		}
   
		public WaitCry(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WaitCry(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WaitCry(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Espera a que acabe la ejecución de Cry.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WaitCry";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

	}
}
