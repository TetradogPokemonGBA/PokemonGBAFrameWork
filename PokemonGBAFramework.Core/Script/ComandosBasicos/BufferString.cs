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
	public class BufferString:Comando,IString
	{
		public const byte ID=0x85;
		public new const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
		public const string NOMBRE="BufferString";
		public const string DESCRIPCION="Guarda una string en el Buffer especificado";

        public BufferString(Byte buffer,BloqueString texto)
		{
			Buffer=buffer;
			Texto=texto;
			
		}
		
		public BufferString(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public BufferString(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe BufferString(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public BloqueString Texto { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Buffer,Texto};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			Texto=BloqueString.Get(ptrRom,new OffsetRom(ptrRom,offsetComando));		
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			data[1]=Buffer;
			OffsetRom.Set(data,2,new OffsetRom(Texto.IdUnicoTemp));
			return data;
		}
	}
}
