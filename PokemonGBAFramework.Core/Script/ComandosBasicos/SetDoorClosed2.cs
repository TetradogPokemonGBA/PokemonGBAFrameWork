/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetDoorClosed2.
	/// </summary>
	public class SetDoorClosed2:SetDoorClosed
	{
		public new const byte ID = 0xB0;
		public new const string NOMBRE = "SetDoorClosed2";
		public new const string DESCRIPCION = "Prepara la puerta para ser cerrada. Sin animación.";


		public SetDoorClosed2() { }
		public SetDoorClosed2(Word coordenadaX, Word coordenadaY):base(coordenadaX,coordenadaY)
		{
 
		}
   
		public SetDoorClosed2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetDoorClosed2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetDoorClosed2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
	
	}
}
