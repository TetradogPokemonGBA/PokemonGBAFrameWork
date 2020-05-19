/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of RemoveCoins.
	/// </summary>
	public class RemoveCoins:Comando
	{
		public const byte ID = 0xB5;
		public const int SIZE = 3;
		Word numeroDeFichasACoger;
 
		public RemoveCoins(Word numeroDeFichasACoger)
		{
			NumeroDeFichasACoger = numeroDeFichasACoger;
 
		}
   
		public RemoveCoins(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public RemoveCoins(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe RemoveCoins(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Coge el numero especificado de fichas del jugador.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "RemoveCoins";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word NumeroDeFichasACoger {
			get{ return numeroDeFichasACoger; }
			set{ numeroDeFichasACoger = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ numeroDeFichasACoger };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			numeroDeFichasACoger = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , NumeroDeFichasACoger);
		}
	}
}
