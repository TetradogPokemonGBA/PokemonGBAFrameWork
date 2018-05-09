/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SetWeather.
	/// </summary>
	public class SetWeather:Comando
	{
		public const byte ID = 0xA4;
		public const int SIZE = 3;
		Word tiempoNuevo;
 
		public SetWeather(Word tiempoNuevo)
		{
			TiempoNuevo = tiempoNuevo;
 
		}
   
		public SetWeather(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SetWeather(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SetWeather(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Prepara la transición al tiempo especificado.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetWeather";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word TiempoNuevo {
			get{ return tiempoNuevo; }
			set{ tiempoNuevo = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ tiempoNuevo };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			tiempoNuevo = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, TiempoNuevo);
		}
	}
}
