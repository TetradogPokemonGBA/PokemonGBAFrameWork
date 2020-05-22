/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Darken.
	/// </summary>
	public class Darken:Comando
	{
		public const byte ID = 0x99;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
        public const string NOMBRE= "Darken";
        public const string DESCRIPCION= "Llama a la animación destello que oscurece el área, deberia ser llamado desde un script de nivel.";

		public Darken() { }
        public Darken(Word tamañoDestello)
		{
			TamañoDestello = tamañoDestello;
 
		}
   
		public Darken(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Darken(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Darken(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Word TamañoDestello { get; set; }

        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ TamañoDestello };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			TamañoDestello = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1, TamañoDestello);
			return data;
		}
	}
}
