/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{//falta añadir enumeracion con los efectos posibles :)
	/// <summary>
	/// Description of Cry.
	/// </summary>
	public class Cry:Comando
	{
		public const byte ID = 0xA1;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH;
        public const string NOMBRE = "Cry";
        public const string DESCRIPCION = "Reproduce el grito del pokemon.";

        public Cry(Word pokemon, Word efecto)
		{
			Pokemon = pokemon;
			Efecto = efecto;
 
		}
   
		public Cry(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Cry(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Cry(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public Word Pokemon { get; set; }
        public Word Efecto { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Pokemon, Efecto };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Efecto = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, , Pokemon);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetData(data, , Efecto);
		}
	}
}
