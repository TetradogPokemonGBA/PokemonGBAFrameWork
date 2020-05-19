/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of AddPcItem.
	/// </summary>
	public class AddPcItem:Comando
	{
		public const byte ID=0x49;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE="AddPcItem";
		public const string DESCRIPCION="Añade la cantidad del objeto especificado en el pc del player";
		public AddPcItem() { }
        public AddPcItem(Word objeto,Word cantidad)
		{
			Objeto=objeto;
			Cantidad=cantidad;
			
		}
		
		public AddPcItem(ScriptManager scriptManager, RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public AddPcItem(ScriptManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe AddPcItem(ScriptManager scriptManager, byte* ptRom,int offset):base(scriptManager, ptRom,offset)
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
        public Word Objeto { get; set; }
        public Word Cantidad { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Objeto,Cantidad};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Objeto=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			Cantidad=new Word(ptrRom,offsetComando);
			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
           data[0]=IdComando;
            Word.SetData(data, 1,Objeto);

			Word.SetData(data,3 ,Cantidad);
			return data;
			
		}
	}
}
