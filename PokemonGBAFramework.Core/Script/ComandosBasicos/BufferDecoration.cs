/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferDecoration.
	/// </summary>
	public class BufferDecoration:Comando
	{
		public const byte ID=0x81;
		public new const int SIZE=Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE="BufferDecoration";
		public const string DESCRIPCION="Guarda el nombre del item decorativo en el Buffer especificado.";

		public BufferDecoration() { }
        public BufferDecoration(byte buffer,Word decoracion)
		{
			Buffer=buffer;
			Decoracion=decoracion;
			
		}
		
		public BufferDecoration(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public BufferDecoration(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe BufferDecoration(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public Word Decoracion { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Buffer,Decoracion};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			Decoracion=new Word(ptrRom,offsetComando);
		
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
           data[0]=IdComando;
            data[1]=Buffer;
			Word.SetData(data,2,Decoracion);
			return data;
		
		}
	}
}
