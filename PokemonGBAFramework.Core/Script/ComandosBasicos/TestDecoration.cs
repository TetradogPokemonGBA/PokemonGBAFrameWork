/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of TestDecoration.
	/// </summary>
	public class TestDecoration:Comando
	{
		public const byte ID = 0x4D;
		public const int SIZE = 3;
		Word decoracion;
 
		public TestDecoration(Word decoracion)
		{
			Decoracion = decoracion;
 
		}
   
		public TestDecoration(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public TestDecoration(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe TestDecoration(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Prueba un objeto decorativo especifico para ver si hay espacio sufieciente para almacenarla";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "TestDecoration";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Decoracion {
			get{ return decoracion; }
			set{ decoracion = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ decoracion };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			decoracion = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			Word.SetData(data, , Decoracion);
		}
	}
}
