/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ShowMoney.
	/// </summary>
	public class ShowMoney:Comando
	{
		public const byte ID = 0x93;
		public const int SIZE = 4;
		Byte coordenadaX;
		Byte coordenadaY;
		Byte comprobarEjecucionComando;
		
		public ShowMoney(Byte coordenadaX, Byte coordenadaY, Byte comprobarEjecucionComando)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			ComprobarEjecucionComando = comprobarEjecucionComando;
			
		}
		
		public ShowMoney(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public ShowMoney(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe ShowMoney(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Muestra en las coordenadas especificadas el dinero que tiene el jugador";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowMoney";
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
		public Byte ComprobarEjecucionComando {
			get{ return comprobarEjecucionComando; }
			set{ comprobarEjecucionComando = value; }
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ coordenadaX, coordenadaY, comprobarEjecucionComando };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			coordenadaX = *(ptrRom + offsetComando);
			offsetComando++;
			coordenadaY = *(ptrRom + offsetComando);
			offsetComando++;
			comprobarEjecucionComando = *(ptrRom + offsetComando);
			offsetComando++;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			*ptrRomPosicionado = coordenadaX;
			++ptrRomPosicionado;
			*ptrRomPosicionado = coordenadaY;
			++ptrRomPosicionado;
			*ptrRomPosicionado = comprobarEjecucionComando;
			++ptrRomPosicionado;
			
		}
	}
}
