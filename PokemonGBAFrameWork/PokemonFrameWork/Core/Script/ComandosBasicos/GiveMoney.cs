/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of GiveMoney.
	/// </summary>
	public class GiveMoney:Comando
	{
		public const byte ID = 0x90;
		public const int SIZE = 6;
		DWord dineroADar;
		Byte comprobarEjecucionComando;
		
		public GiveMoney(DWord dineroADar, Byte comprobarEjecucionComando)
		{
			DineroADar = dineroADar;
			ComprobarEjecucionComando = comprobarEjecucionComando;
			
		}
		
		public GiveMoney(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public GiveMoney(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe GiveMoney(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Da al jugador algo de dinero.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "GiveMoney";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public DWord DineroADar {
			get{ return dineroADar; }
			set{ dineroADar = value; }
		}
		public Byte ComprobarEjecucionComando {
			get{ return comprobarEjecucionComando; }
			set{ comprobarEjecucionComando = value; }
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ dineroADar, comprobarEjecucionComando };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			dineroADar = new DWord(ptrRom , offsetComando);
			offsetComando += DWord.LENGTH;
			comprobarEjecucionComando = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			DWord.SetData(ptrRomPosicionado, DineroADar);
			ptrRomPosicionado += DWord.LENGTH;
			*ptrRomPosicionado = comprobarEjecucionComando;
		}
	}
}
