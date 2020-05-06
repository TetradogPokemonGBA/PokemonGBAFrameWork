/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferString.
	/// </summary>
	public class BufferString:Comando
	{
		public const byte ID=0x85;
		public new const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
		public const string NOMBRE="BufferString";
		public const string DESCRIPCION="Guarda una string en el Buffer especificado";

        public BufferString(Byte buffer,OffsetRom texto)
		{
			Buffer=buffer;
			String=texto;
			
		}
		
		public BufferString(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferString(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferString(byte* ptRom,int offset):base(ptRom,offset)
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
        public OffsetRom String { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Buffer,String};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			String=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);		
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=Buffer;
			++ptrRomPosicionado;
			OffsetRom.SetOffset(ptrRomPosicionado,String);		
		}
	}
}
