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

		public Cry() { }
        public Cry(Word pokemon, Word efecto)
		{
			Pokemon = pokemon;
			Efecto = efecto;
 
		}
   
		public Cry(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Cry(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Cry(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Efecto = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1, Pokemon);
 
			Word.SetData(data,3, Efecto);
			return data;
		}
	}
}
