/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ShowPokePic.
	/// </summary>
	public class ShowPokePic:Comando
	{
		public const byte ID = 0x75;
		public const int SIZE = 5;
		Word pokemon;
		Byte coordenadaX;
		Byte coordenadaY;
 
		public ShowPokePic(Word pokemon, Byte coordenadaX, Byte coordenadaY)
		{
			Pokemon = pokemon;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public ShowPokePic(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public ShowPokePic(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe ShowPokePic(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Muestra un pokemon en una caja";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowPokePic";
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
		public Byte CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Byte CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ pokemon, coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaX = *(ptrRom + offsetComando);
			offsetComando++;
			coordenadaY = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado, Pokemon);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = coordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = coordenadaY;
		}
	}
}
