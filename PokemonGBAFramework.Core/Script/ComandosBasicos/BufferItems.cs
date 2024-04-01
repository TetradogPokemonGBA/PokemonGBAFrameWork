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

		public BufferItems() { }
        public BufferItems(Byte buffer, Word objetoAGuardar, Word cantidad):base(buffer,objetoAGuardar)
		{
			Cantidad = cantidad;

		}
   
		public BufferItems(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public BufferItems(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe BufferItems(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
        protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.VerdeHoja|Edicion.Pokemon.RojoFuego;
		}
		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			var lst= base.GetParams();
			lst.Add(new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Cantidad))) ;
			return lst;
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
            base.CargarCamando(scriptManager,ptrRom, offsetComando);
			offsetComando += base.ParamsSize;
			Cantidad = new Word(ptrRom, offsetComando);
	}
		public override byte[] GetBytesTemp()
		{
			return base.GetBytesTemp().AddArray(Cantidad.Data);

		}
	}
}
