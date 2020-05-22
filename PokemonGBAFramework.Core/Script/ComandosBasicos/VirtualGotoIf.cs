/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualGotoIf.
	/// </summary>
	public class VirtualGotoIf:VirtualCallIf
	{
		public new const byte ID = 0xBB;
		public new const string NOMBRE = "VirtualGotoIf";
		public new const string DESCRIPCION = "Salta asta la función si cumple con la condición.";



		public VirtualGotoIf() { }
		public VirtualGotoIf(Byte condicion, Script funcionPersonalizada):base(condicion,funcionPersonalizada)
		{

 
		}
   
		public VirtualGotoIf(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualGotoIf(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualGotoIf(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;


	}
}
