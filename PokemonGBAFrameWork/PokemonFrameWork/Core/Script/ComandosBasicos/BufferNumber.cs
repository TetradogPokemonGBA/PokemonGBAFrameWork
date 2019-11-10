/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferNumber.
	/// </summary>
	public class BufferNumber:Comando
	{
		public const byte ID=0x83;
		public new const int SIZE=Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE="BufferNumber";
		public const string DESCRIPCION="Variable version on buffernumber.";

        public BufferNumber(Byte buffer,Word variableToStore)
		{
			Buffer=buffer;
			VariableToStore=variableToStore;
			
		}
		
		public BufferNumber(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferNumber(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferNumber(byte* ptRom,int offset):base(ptRom,offset)
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
        public Word VariableToStore { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Buffer,VariableToStore};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			VariableToStore=new Word(ptrRom,offsetComando);
		
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=Buffer;
			++ptrRomPosicionado;
			Word.SetData(ptrRomPosicionado,VariableToStore);
		}
	}
}
