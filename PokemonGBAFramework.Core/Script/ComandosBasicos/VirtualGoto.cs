/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualGoto.
	/// </summary>
	public class VirtualGoto:VirtualCall
	{
		public new const byte ID = 0xB9;
		public new const string NOMBRE = "VirtualGoto";
		public new const string DESCRIPCION = "Salta asta la función especificada.";



		public VirtualGoto() { }
		public VirtualGoto(Script funcionPersonalizada):base(funcionPersonalizada)
		{
 
		}
   
		public VirtualGoto(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualGoto(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualGoto(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}


		#region IEndScript implementation
		public override bool IsEnd => true;
		#endregion
	
	}
}
