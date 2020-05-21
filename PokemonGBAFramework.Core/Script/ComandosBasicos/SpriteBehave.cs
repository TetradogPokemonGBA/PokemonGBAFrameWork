/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SpriteBehave.
	/// </summary>
	public class SpriteBehave:Comando
	{
		public const byte ID = 0x65;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+1;
		public const string NOMBRE = "SpriteBehave";
		public const string DESCRIPCION = "Cambia el comportamiento de un sprite";

		public SpriteBehave() { }
		public SpriteBehave(Word personaje, Byte comportamiento)
		{
			Personaje = personaje;
			Comportamiento = comportamiento;
 
		}
   
		public SpriteBehave(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteBehave(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteBehave(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Word Personaje { get; set; }
		public Byte Comportamiento { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Personaje, Comportamiento };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Comportamiento = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, Personaje);
			data[3] = Comportamiento;

			return data;
		}
	}
}
