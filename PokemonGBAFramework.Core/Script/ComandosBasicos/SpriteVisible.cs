/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SpriteVisible.
	/// </summary>
	public class SpriteVisible:Comando
	{
		public const byte ID = 0x58;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE = "SpriteVisible";
		public const string DESCRIPCION = "Hace visible un sprite del mapa y bank seleccionado";

		public SpriteVisible() { }
		public SpriteVisible(Word personaje, Word bank, Word mapa)
		{
			Personaje = personaje;
			Bank = bank;
			Mapa = mapa;
 
		}
   
		public SpriteVisible(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteVisible(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteVisible(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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

		public override System.Collections.Generic.IList<object> GetParams()
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
