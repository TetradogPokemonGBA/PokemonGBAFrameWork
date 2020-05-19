/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of UpdateCoins.
	/// </summary>
	public class UpdateCoins:Comando
	{
		public const byte ID = 0xC2;
		public const int SIZE = 3;
		Byte coordenadaX;
		Byte coordenadaY;
 
		public UpdateCoins(Byte coordenadaX, Byte coordenadaY)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public UpdateCoins(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public UpdateCoins(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe UpdateCoins(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Actualiza el numero de fichas de la pantalla.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "UpdateCoins";
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
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			coordenadaX = ptrRom[offsetComando];
			offsetComando++;
			coordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			*ptrRomPosicionado = coordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = coordenadaY;
		}
	}
}
