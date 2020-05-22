/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckTrainerFlag.
	/// </summary>
	public class CheckTrainerFlag:Comando
	{
		public const byte ID = 0x60;
        public const string NOMBRE = "CheckTrainerFlag";
        public new const int SIZE = Comando.SIZE+Word.LENGTH;
        public const string DESCRIPCION= "Comprueba si el flag del entrenador esta activado y guarda el resultado en LASTRESULT";

		public CheckTrainerFlag() { }
        public CheckTrainerFlag(Word entrenador)
		{
			Entrenador = entrenador;
 
		}
   
		public CheckTrainerFlag(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CheckTrainerFlag(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CheckTrainerFlag(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Word Entrenador { get; set; }

        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Entrenador };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Entrenador = new Word(ptrRom, offsetComando);

		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1 , Entrenador);
			return data;
		}
	}
}
