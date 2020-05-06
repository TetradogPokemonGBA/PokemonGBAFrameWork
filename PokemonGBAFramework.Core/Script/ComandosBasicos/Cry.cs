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
   
		public Cry(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Cry(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Cry(byte* ptRom, int offset)
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
        public Word Pokemon { get; set; }
        public Word Efecto { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Pokemon, Efecto };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Efecto = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado, Pokemon);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetData(ptrRomPosicionado, Efecto);
		}
	}
}
