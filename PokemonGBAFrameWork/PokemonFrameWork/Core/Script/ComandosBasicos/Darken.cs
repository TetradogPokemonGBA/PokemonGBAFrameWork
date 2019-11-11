/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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
        public Darken(Word tamañoDestello)
		{
			TamañoDestello = tamañoDestello;
 
		}
   
		public Darken(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Darken(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Darken(byte* ptRom, int offset)
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
        public Word TamañoDestello { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ TamañoDestello };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			TamañoDestello = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado, TamañoDestello);
		}
	}
}
