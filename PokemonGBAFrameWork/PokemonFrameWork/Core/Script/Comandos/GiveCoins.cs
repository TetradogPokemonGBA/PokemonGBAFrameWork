/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of GiveCoins.
	/// </summary>
	public class GiveCoins:Comando
	{
		public const byte ID = 0xB4;
		public const int SIZE = 3;
		Word numeroDeFichasADar;
 
		public GiveCoins(Word numeroDeFichasADar)
		{
			NumeroDeFichasADar = numeroDeFichasADar;
 
		}
   
		public GiveCoins(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public GiveCoins(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe GiveCoins(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Da al jugador el numero especificado de fichas.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "GiveCoins";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word NumeroDeFichasADar {
			get{ return numeroDeFichasADar; }
			set{ numeroDeFichasADar = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ numeroDeFichasADar };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			numeroDeFichasADar = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado, NumeroDeFichasADar);
		}
	}
}
