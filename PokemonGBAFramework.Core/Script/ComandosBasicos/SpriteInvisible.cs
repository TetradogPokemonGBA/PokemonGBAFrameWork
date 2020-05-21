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
		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE = "SpriteInvisible";
		public const string DESCRIPCION = "Hace invisible el personaje especificado del mapa y banco";

		public SpriteInvisible() { }
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
		public Word Bank { get; set; }
		public Word Mapa { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Personaje, Bank, Mapa };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Bank = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Mapa = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, Personaje);
			Word.SetData(data,3, Bank);
			Word.SetData(data,5, Mapa);

			return data;
		}
	}
}
