/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Warp7.
	/// </summary>
	public class Warp7:Warp
	{
		public new const byte ID = 0xD7;
		public new const string NOMBRE = "Warp7";
		public new const string DESCRIPCION = "bajo investigación.";

		public Warp7() { }
		public Warp7(Byte bancoAIr, Byte mapaAIr, Byte salidaAIr, Word coordenadaX, Word coordenadaY):base(bancoAIr,mapaAIr,salidaAIr,coordenadaX,coordenadaY)
		{

 
		}
   
		public Warp7(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Warp7(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Warp7(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;


	}
}
