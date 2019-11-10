/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckItem.
	/// </summary>
	public class CheckItem:Comando
	{
		public const byte ID = 0x47;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE="CheckItem";
		public const string DESCRIPCION="Comprueba si el player lleva la cantidad del objeto especificado";

        public CheckItem(Word objeto, Word cantidad)
		{
			Objeto = objeto;
			Cantidad = cantidad;
 
		}
   
		public CheckItem(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CheckItem(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CheckItem(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
        public Word Objeto { get; set; }
        public Word Cantidad { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Objeto, Cantidad };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Objeto = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Cantidad = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado, Objeto);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetData(ptrRomPosicionado, Cantidad); 
		}
	}
}
