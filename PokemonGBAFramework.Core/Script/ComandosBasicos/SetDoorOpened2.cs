/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetDoorOpened2.
	/// </summary>
	public class SetDoorOpened2:SetDoorClosed
	{
		public new const byte ID = 0xAF;
		public new const string NOMBRE = "SetDoorOpened2";
		public new const string DESCRIPCION = "Prepara la puerta para ser abierta. Sin animacion.";


		public SetDoorOpened2() { }
		public SetDoorOpened2(Word coordenadaX, Word coordenadaY):base(coordenadaX,coordenadaY)
		{

 
		}
   
		public SetDoorOpened2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetDoorOpened2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetDoorOpened2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
