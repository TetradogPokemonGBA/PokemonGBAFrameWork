/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckItemType.
	/// </summary>
	public class CheckItemType:Comando
	{
		public const byte ID = 0x48;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
		public const string NOMBRE="CheckItemType";
		public const string DESCRIPCION="Comprueba el tipo del objeto, el resultado se guarda en LASTRESULT";

        public CheckItemType(Word objeto)
		{
			Objeto = objeto;
 
		}
   
		public CheckItemType(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CheckItemType(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CheckItemType(byte* ptRom, int offset)
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

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Objeto };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Objeto = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado, Objeto);
		}
	}
}
