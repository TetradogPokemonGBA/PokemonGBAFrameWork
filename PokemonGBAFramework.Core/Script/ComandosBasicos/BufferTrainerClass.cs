/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public BufferTrainerClass(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public BufferTrainerClass(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe BufferTrainerClass(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Word ClaseEntrenador { get; set; }
        protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer, ClaseEntrenador };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer = ptrRom[offsetComando];
			offsetComando++;
			ClaseEntrenador = new Word(ptrRom, offsetComando); 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			data[1] = Buffer;
			Word.SetData(data,2 , ClaseEntrenador);
			return data;

		}
	}
}
