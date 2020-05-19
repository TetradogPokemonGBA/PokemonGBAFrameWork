/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferTrainerName.
	/// </summary>
	public class BufferTrainerName:Comando
	{
		public const byte ID = 0xDE;
		public new const int SIZE = Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE="BufferTrainerName";
		public const string DESCRIPCION="Guarda en el buffer el nombre del entrenador.";

        public BufferTrainerName(Byte buffer, Word entrenador)
		{
			Buffer = buffer;
			Entrenador = entrenador;
 
		}
   
		public BufferTrainerName(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public BufferTrainerName(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe BufferTrainerName(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public Word Entrenador { get; set; }
        protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer, Entrenador };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer = ptrRom[offsetComando];
			offsetComando++;
			Entrenador = new Word(ptrRom, offsetComando);

		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = Buffer;
			++ptrRomPosicionado; 
			Word.SetData(data, , Entrenador);
 
		}
	}
}
