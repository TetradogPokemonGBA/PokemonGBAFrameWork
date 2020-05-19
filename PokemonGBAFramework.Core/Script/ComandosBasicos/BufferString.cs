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
		
		public BufferString(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public BufferString(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe BufferString(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			String=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);		
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=Buffer;
			++ptrRomPosicionado;
			OffsetRom.Set(ptrRomPosicionado,String);		
		}
	}
}
