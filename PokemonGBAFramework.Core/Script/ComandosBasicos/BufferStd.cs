/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferStd.
	/// </summary>
	public class BufferStd:Comando
	{
		public const byte ID = 0x84;
		public new const int SIZE = Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE="BufferStd";
		public const string DESCRIPCION="Guarda una string estandar en el buffer especificado.";

		public BufferStd() { }
        public BufferStd(Byte buffer, Word standarString)
		{
			Buffer = buffer;
			StandarString = standarString;
 
		}
   
		public BufferStd(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public BufferStd(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe BufferStd(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
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
        public Word StandarString { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer, StandarString };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer = ptrRom[offsetComando];
			offsetComando++;
			StandarString = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			data[1] = Buffer;
			Word.SetData(data,2, StandarString);
			return data;
 
		}
	}
}
