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
		public const int SIZE = 4;
		Word personaje;
		Byte comportamiento;
 
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
				return "Cambia el comportamiento de un sprite";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SpriteBehave";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Personaje {
			get{ return personaje; }
			set{ personaje = value; }
		}
		public Byte Comportamiento {
			get{ return comportamiento; }
			set{ comportamiento = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personaje, comportamiento };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			comportamiento = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			Word.SetData(data, , Personaje);
 
			*ptrRomPosicionado = comportamiento;
		}
	}
}
