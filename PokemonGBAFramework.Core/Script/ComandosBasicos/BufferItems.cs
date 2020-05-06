/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using Gabriel.Cat.S.Extension;
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferItems.
	/// </summary>
	public class BufferItems:BufferItem
	{
		public new const byte ID = 0xD4;
		public new const int SIZE = BufferItem.SIZE+Word.LENGTH;
		public new const string NOMBRE="BufferItems";
		public new const string DESCRIPCION="Stores a plural item name within a specified buffer.";

        public BufferItems(Byte buffer, Word objetoAGuardar, Word cantidad):base(buffer,objetoAGuardar)
		{
			Cantidad = cantidad;

		}
   
		public BufferItems(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public BufferItems(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe BufferItems(byte* ptRom, int offset)
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

        public Word Cantidad { get; set; }
        protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPG|AbreviacionCanon.BPR;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return base.GetParams().AfegirValor(Cantidad) ;
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
            base.CargarCamando(ptrRom, offsetComando);
			offsetComando += base.ParamsSize;
			Cantidad = new Word(ptrRom, offsetComando);
	}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
        
			ptrRomPosicionado += BufferItem.SIZE;
			Word.SetData(ptrRomPosicionado, Cantidad);

		}
	}
}
