/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Warp8.
	/// </summary>
	public class Warp8:Warp
	{
		public new const byte ID = 0xE0;
		public new const string NOMBRE = "Warp8";
		public new const string DESCRIPCION = "bajo investigación.";
		public Warp8() { }
		public Warp8(Byte bancoAIr, Byte mapaAIr, Byte salidaAIr, Word coordenadaX, Word coordenadaY) : base(bancoAIr, mapaAIr, salidaAIr, coordenadaX, coordenadaY)
		{


		}


		public Warp8(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Warp8(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Warp8(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;


	}
}
