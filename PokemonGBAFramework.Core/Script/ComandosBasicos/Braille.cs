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
	public class Braille:Comando,IBraille
	{
		public const byte ID = 0x78;
		public new const int SIZE = Comando.SIZE+OffsetRom.LENGTH;
		public const string NOMBRE = "Braille";
		public const string DESCRIPCION = "Muestra una caja con texto en braille( no soporta '\\l','\\p','\\n')";
		public Braille() { }
        public Braille(BloqueBraille brailleData)
		{
			BrailleData = brailleData;
			
		}
		
		public Braille(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			: base(scriptManager, rom, offset)
		{
		}
		public Braille(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset)
			: base(scriptManager, bytesScript, offset)
		{
		}
		public unsafe Braille(ScriptAndASMManager scriptManager, byte* ptRom, int offset)
			: base(scriptManager, ptRom, offset)
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
        public BloqueBraille BrailleData { get; set; }

        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ BrailleData };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			BrailleData =new BloqueBraille(ptrRom, new OffsetRom(ptrRom, offsetComando));
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			OffsetRom.Set(data,1,new OffsetRom(BrailleData.IdUnicoTemp));
			return data;
		}
	}
}
