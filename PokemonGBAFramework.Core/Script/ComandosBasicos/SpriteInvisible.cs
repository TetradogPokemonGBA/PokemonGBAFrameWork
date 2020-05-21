/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SpriteInvisible.
	/// </summary>
	public class SpriteInvisible:Comando
	{
		public const byte ID = 0x59;
		public const int SIZE = 5;
		Word personaje;
		Word bank;
		Word mapa;
 
		public SpriteInvisible(Word personaje, Word bank, Word mapa)
		{
			Personaje = personaje;
			Bank = bank;
			Mapa = mapa;
 
		}
   
		public SpriteInvisible(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteInvisible(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteInvisible(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Hace invisible el personaje especificado del mapa y banco";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SpriteInvisible";
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
		public Word Bank {
			get{ return bank; }
			set{ bank = value; }
		}
		public Word Mapa {
			get{ return mapa; }
			set{ mapa = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personaje, bank, mapa };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			bank = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			mapa = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			Word.SetData(data, , Personaje);
 
			Word.SetData(data, , Bank);
 
			Word.SetData(data, , Mapa);
		}
	}
}
