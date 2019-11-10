/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferTrainerClass.
	/// </summary>
	public class BufferTrainerClass:Comando
	{
		public const byte ID = 0xDD;
		public new const int SIZE = Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE="BufferTrainerClass";
		public const string DESCRIPCION="Guarda en el buffer el nombre de la clase de entrenador especificada.";

        public BufferTrainerClass(Byte buffer, Word claseEntrenador)
		{
			Buffer = buffer;
			ClaseEntrenador = claseEntrenador;
 
		}
   
		public BufferTrainerClass(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public BufferTrainerClass(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe BufferTrainerClass(byte* ptRom, int offset)
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
        public Word ClaseEntrenador { get; set; }
        protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPE;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer, ClaseEntrenador };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer = *(ptrRom + offsetComando);
			offsetComando++;
			ClaseEntrenador = new Word(ptrRom, offsetComando); 
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = Buffer;
			++ptrRomPosicionado; 
			Word.SetData(ptrRomPosicionado, ClaseEntrenador);

		}
	}
}
