/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferPartyPokemon.
	/// </summary>
	public class BufferPartyPokemon:Comando
	{
		public const byte ID = 0x7F;
		public const int SIZE = 4;
		Byte buffer;
		Word pokemon;
 
		public BufferPartyPokemon(Byte buffer, Word pokemon)
		{
			Buffer = buffer;
			Pokemon = pokemon;
 
		}
   
		public BufferPartyPokemon(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public BufferPartyPokemon(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe BufferPartyPokemon(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Guarda el nombre del pokemon seleccionado del equipo en el Buffer especificado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "BufferPartyPokemon";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Buffer {
			get{ return buffer; }
			set{ buffer = value; }
		}
		public Word Pokemon {
			get{ return pokemon; }
			set{ pokemon = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ buffer, pokemon };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			buffer = *(ptrRom + offsetComando);
			offsetComando++;
			pokemon = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = buffer;
			++ptrRomPosicionado; 
			Word.SetWord(ptrRomPosicionado, Pokemon);
		}
	}
}
