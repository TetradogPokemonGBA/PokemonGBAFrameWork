/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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

		public CheckItem() { }
        public CheckItem(Word objeto, Word cantidad)
		{
			Objeto = objeto;
			Cantidad = cantidad;
 
		}
   
		public CheckItem(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CheckItem(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CheckItem(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Word Objeto { get; set; }
        public Word Cantidad { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Objeto, Cantidad };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Objeto = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Cantidad = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1, Objeto);
			Word.SetData(data,3, Cantidad);
			return data;
		}
	}
}
