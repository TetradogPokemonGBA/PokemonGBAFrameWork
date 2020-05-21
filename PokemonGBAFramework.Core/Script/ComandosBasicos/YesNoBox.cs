/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of YesNoBox.
	/// </summary>
	public class YesNoBox:Comando
	{
		public const byte ID = 0x6E;
		public const int SIZE = 3;
		Byte coordenadaX;
		Byte coordenadaY;
		
		public YesNoBox(Byte coordenadaX, Byte coordenadaY)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			
		}
		
		public YesNoBox(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public YesNoBox(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe YesNoBox(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Muestra una caja Si/No en las especificas coordenadas";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "YesNoBox";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Byte CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			coordenadaX = ptrRom[offsetComando];
			offsetComando++;
			coordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			*ptrRomPosicionado = coordenadaX;
			++ptrRomPosicionado;
			*ptrRomPosicionado = coordenadaY;
		}
	}
}
