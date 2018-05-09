/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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
   
		public ShowBox(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public ShowBox(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe ShowBox(byte* ptRom, int offset)
			: base(ptRom, offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			posicionX = *(ptrRom + offsetComando);
			offsetComando++;
			posicionY = *(ptrRom + offsetComando);
			offsetComando++;
			ancho = *(ptrRom + offsetComando);
			offsetComando++;
			alto = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
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
