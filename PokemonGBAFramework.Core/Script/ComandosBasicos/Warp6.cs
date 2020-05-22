/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Warp6.
	/// </summary>
	public class Warp6:Warp
	{
		public new const byte ID = 0xC4;
		public new const string NOMBRE = "Warp6";
		public new const string DESCRIPCION = "Transporta al jugador a otro mapa.";

		public Warp6() { }
		public Warp6(Byte bancoAIr, Byte mapaAIr, Byte salidaAIr, Word coordenadaX, Word coordenadaY):base(bancoAIr,mapaAIr,salidaAIr,coordenadaX,coordenadaY)
		{
 
		}
   
		public Warp6(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Warp6(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Warp6(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;

	}
}
