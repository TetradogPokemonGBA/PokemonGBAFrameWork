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

        public BufferContestType(Byte buffer, Word tipoConcurso)
		{
			Buffer = buffer;
			TipoConcurso = tipoConcurso;
 
		}
   
		public BufferContestType(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public BufferContestType(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe BufferContestType(byte* ptRom, int offset)
			: base(ptRom, offset)
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
        public Word TipoConcurso { get; set; }
        protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer, TipoConcurso };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer = ptrRom[offsetComando];
			offsetComando++;
			TipoConcurso = new Word(ptrRom, offsetComando);

		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
            ptrRomPosicionado += base.Size;
            *ptrRomPosicionado = Buffer;
			++ptrRomPosicionado; 
			Word.SetData(ptrRomPosicionado, TipoConcurso);
		}
	}
}
