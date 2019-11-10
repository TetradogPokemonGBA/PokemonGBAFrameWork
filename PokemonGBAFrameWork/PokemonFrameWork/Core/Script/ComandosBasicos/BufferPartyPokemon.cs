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
		public new const int SIZE = Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE="BufferPartyPokemon";
		public const string DESCRIPCION="Guarda el nombre del pokemon seleccionado del equipo en el Buffer especificado";

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
        public Byte Buffer { get; set; }
        public Word Pokemon { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer, Pokemon };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer = *(ptrRom + offsetComando);
			offsetComando++;
			Pokemon = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = Buffer;
			++ptrRomPosicionado; 
			Word.SetData(ptrRomPosicionado, Pokemon);
		}
	}
}
