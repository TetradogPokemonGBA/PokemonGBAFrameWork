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
		public const int SIZE=4;
		Byte buffer;
		ushort itemToStore;
		
		public BufferItem(Byte buffer,ushort itemToStore)
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
				return "Guarda el nombre del objeto en el Buffer";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "BufferItem";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Buffer
		{
			get{ return buffer;}
			set{buffer=value;}
		}
		public ushort ItemToStore
		{
			get{ return itemToStore;}
			set{itemToStore=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{buffer,itemToStore};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			buffer=*(ptrRom+offsetComando);
			offsetComando++;
			itemToStore=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			*ptrRomPosicionado=buffer;
			++ptrRomPosicionado;
			Word.SetWord(ptrRomPosicionado,ItemToStore);
			ptrRomPosicionado+=Word.LENGTH;
			
		}
	}
}