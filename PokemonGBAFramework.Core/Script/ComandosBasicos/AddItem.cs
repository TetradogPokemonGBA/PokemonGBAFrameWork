/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of AddItem.
	/// </summary>
	public class AddItem:Comando
	{
		public const byte ID=0x44;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE="AddItem";
		public const string DESCRIPCION="Añade la cantidad del objeto especificado";

		public AddItem() { }
        public AddItem(Word objetoAAñadir,Word cantidad)
		{
			ObjetoAAñadir=objetoAAñadir;

			Cantidad=cantidad;

			
		}
		
		public AddItem(ScriptAndASMManager scriptManager, RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public AddItem(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe AddItem(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
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


        public Word ObjetoAAñadir { get; set; }

        public Word Cantidad { get; set; }
        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ObjetoAAñadir,Cantidad};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			ObjetoAAñadir=new Word(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			Cantidad=new Word(ptrRom,offsetComando);
		
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			Word.SetData(data, 1, ObjetoAAñadir);
			Word.SetData(data, 3, Cantidad);
			return data;
		}
	}
}
