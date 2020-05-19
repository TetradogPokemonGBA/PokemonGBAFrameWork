/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferContestType.
	/// </summary>
	public class BufferContestType:Comando
	{
		public const byte ID = 0xE1;
		public new const int SIZE = Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE = "BufferContestType";
		public const string DESCRIPCION = "Guarda el nombre del concurso seleccionado en el buffer especificado.";

		public BufferContestType() { }
        public BufferContestType(Byte buffer, Word tipoConcurso)
		{
			Buffer = buffer;
			TipoConcurso = tipoConcurso;
 
		}
   
		public BufferContestType(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public BufferContestType(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe BufferContestType(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public byte Buffer { get; set; }
        public Word TipoConcurso { get; set; }
        protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer, TipoConcurso };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer = ptrRom[offsetComando];
			offsetComando++;
			TipoConcurso = new Word(ptrRom, offsetComando);

		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			data[1] = Buffer;
			Word.SetData(data, 2, TipoConcurso);
			return data;
		}
	}
}
