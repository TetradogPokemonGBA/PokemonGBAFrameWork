/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetDoorOpened.
	/// </summary>
	public class SetDoorOpened:SetDoorClosed
	{
		public new const byte ID = 0xAC;
		public new const string NOMBRE = "SetDoorOpened";
		public new const string DESCRIPCION = "Prepara la puerta para ser abierta";

		public SetDoorOpened() { }
 
		public SetDoorOpened(Word coordenadaX, Word coordenadaY):base(coordenadaX,coordenadaY)
		{

 
		}
   
		public SetDoorOpened(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetDoorOpened(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetDoorOpened(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
