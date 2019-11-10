/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

        public BufferDecoration(Byte buffer,Word decoracion)
		{
			Buffer=buffer;
			Decoracion=decoracion;
			
		}
		
		public BufferDecoration(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferDecoration(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferDecoration(byte* ptRom,int offset):base(ptRom,offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			Decoracion=new Word(ptrRom,offsetComando);
		
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
            ptrRomPosicionado += base.Size;
            *ptrRomPosicionado=Buffer;
			++ptrRomPosicionado;
			Word.SetData(ptrRomPosicionado,Decoracion);
		
		}
	}
}
