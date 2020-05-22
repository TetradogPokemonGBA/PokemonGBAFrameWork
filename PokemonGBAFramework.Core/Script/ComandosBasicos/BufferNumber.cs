/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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

		public BufferNumber() { }
        public BufferNumber(Byte buffer,Word variableToStore)
		{
			Buffer=buffer;
			VariableToStore=variableToStore;
			
		}
		
		public BufferNumber(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public BufferNumber(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe BufferNumber(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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

        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Buffer,VariableToStore};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			VariableToStore=new Word(ptrRom,offsetComando);
		
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			data[1]=Buffer;
			Word.SetData(data,2,VariableToStore);
			return data;
		}
	}
}
