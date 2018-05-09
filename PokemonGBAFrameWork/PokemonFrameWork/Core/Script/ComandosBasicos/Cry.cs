/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{//falta añadir enumeracion con los efectos posibles :)
	/// <summary>
	/// Description of Cry.
	/// </summary>
	public class Cry:Comando
	{
		public const byte ID = 0xA1;
		public const int SIZE = 5;
		Word pokemon;
		Word efecto;
 
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
				return "Reproduce el grito del pokemon.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Cry";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Pokemon {
			get{ return pokemon; }
			set{ pokemon = value; }
		}
		public Word Efecto {
			get{ return efecto; }
			set{ efecto = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ pokemon, efecto };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			efecto = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado, Pokemon);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetWord(ptrRomPosicionado, Efecto);
		}
	}
}
