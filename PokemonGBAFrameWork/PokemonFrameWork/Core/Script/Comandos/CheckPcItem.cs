/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckPcItem.
	/// </summary>
	public class CheckPcItem:Comando
	{
		public const byte ID = 0x4A;
		public const int SIZE = 5;
		Word objeto;
		Word cantidad;
 
		public CheckPcItem(Word objeto, Word cantidad)
		{
			Objeto = objeto;
			Cantidad = cantidad;
 
		}
   
		public CheckPcItem(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CheckPcItem(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CheckPcItem(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Mira si el player posee en su pc la cantidad del objeto especificado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "CheckPcItem";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Objeto {
			get{ return objeto; }
			set{ objeto = value; }
		}
		public Word Cantidad {
			get{ return cantidad; }
			set{ cantidad = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ objeto, cantidad };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			objeto = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			cantidad = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado, Objeto);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetWord(ptrRomPosicionado, Cantidad);
		}
	}
}
