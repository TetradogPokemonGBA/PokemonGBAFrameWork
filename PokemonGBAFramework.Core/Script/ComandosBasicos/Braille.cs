/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Braille.
	/// </summary>
	public class Braille:Comando
	{
		public const byte ID = 0x78;
		public new const int SIZE = Comando.SIZE+OffsetRom.LENGTH;
		public const string NOMBRE = "Braille";
		public const string DESCRIPCION = "Muestra una caja con texto en braille( no soporta '\\l','\\p','\\n')";

        public Braille(OffsetRom brailleData)
		{
			BrailleData = brailleData;
			
		}
		
		public Braille(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Braille(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Braille(byte* ptRom, int offset)
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
        public OffsetRom BrailleData { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ BrailleData };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			BrailleData = new OffsetRom(ptrRom, offsetComando);

			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			OffsetRom.SetOffset(ptrRomPosicionado, BrailleData);
			
		}
	}
}
