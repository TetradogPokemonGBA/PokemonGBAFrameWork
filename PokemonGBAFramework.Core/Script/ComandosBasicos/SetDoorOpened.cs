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
	public class SetDoorOpened:Comando
	{
		public const byte ID = 0xAC;
		public const int SIZE = 5;
		Word coordenadaX;
		Word coordenadaY;
 
		public SetDoorOpened(Word coordenadaX, Word coordenadaY)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
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
				return "Prepara la puerta para ser abierta";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetDoorOpened";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Word CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			coordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaY = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , CoordenadaX);
 
			Word.SetData(data, , CoordenadaY);
		}
	}
}
