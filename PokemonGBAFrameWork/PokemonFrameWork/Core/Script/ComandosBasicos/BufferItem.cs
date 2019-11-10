/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

        public BufferItem(Byte buffer,Word itemToStore)
		{
			Buffer=buffer;
			ItemToStore=itemToStore;
			
		}
		
		public BufferItem(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferItem(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferItem(byte* ptRom,int offset):base(ptRom,offset)
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
        public Byte Buffer { get; set; }
        public Word ItemToStore { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Buffer,ItemToStore};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			ItemToStore=new Word(ptrRom,offsetComando);
		
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
            ptrRomPosicionado += base.Size;
            *ptrRomPosicionado=Buffer;
			++ptrRomPosicionado;
			Word.SetData(ptrRomPosicionado,ItemToStore);
		
		}
	}
}
