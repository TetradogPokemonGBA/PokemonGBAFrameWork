/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferItem.
	/// </summary>
	public class BufferItem:Comando
	{
		public const byte ID=0x80;
		public new const int SIZE=Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE="BufferItem";
		public const string DESCRIPCION="Guarda el nombre del objeto en el Buffer";

		public BufferItem() { }
        public BufferItem(Byte buffer,Word itemToStore)
		{
			Buffer=buffer;
			ItemToStore=itemToStore;
			
		}
		
		public BufferItem(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public BufferItem(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe BufferItem(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public byte Buffer { get; set; }
        public Word ItemToStore { get; set; }

        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Buffer)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(ItemToStore))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			ItemToStore=new Word(ptrRom,offsetComando);
		
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			data[1] = Buffer;
			Word.SetData(data, 2, ItemToStore);
			return data;
		}
	}
}
