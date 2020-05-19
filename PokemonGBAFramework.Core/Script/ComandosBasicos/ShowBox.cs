/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowBox.
	/// </summary>
	public class ShowBox:Comando
	{
		public const byte ID = 0x72;
		public const int SIZE = 5;
		Byte posicionX;
		Byte posicionY;
		Byte ancho;
		Byte alto;
 
		public ShowBox(Byte posicionX, Byte posicionY, Byte ancho, Byte alto)
		{
			PosicionX = posicionX;
			PosicionY = posicionY;
			Ancho = ancho;
			Alto = alto;
 
		}
   
		public ShowBox(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowBox(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowBox(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Muestra una caja en la posición y con las medidas especificadas";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowBox";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte PosicionX {
			get{ return posicionX; }
			set{ posicionX = value; }
		}
		public Byte PosicionY {
			get{ return posicionY; }
			set{ posicionY = value; }
		}
		public Byte Ancho {
			get{ return ancho; }
			set{ ancho = value; }
		}
		public Byte Alto {
			get{ return alto; }
			set{ alto = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ posicionX, posicionY, ancho, alto };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			posicionX = ptrRom[offsetComando];
			offsetComando++;
			posicionY = ptrRom[offsetComando];
			offsetComando++;
			ancho = ptrRom[offsetComando];
			offsetComando++;
			alto = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			*ptrRomPosicionado = posicionX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = posicionY;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = ancho;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = alto;
		}
	}
}
