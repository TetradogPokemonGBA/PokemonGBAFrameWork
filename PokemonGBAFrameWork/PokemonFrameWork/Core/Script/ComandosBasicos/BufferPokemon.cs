/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferPokemon.
	/// </summary>
	public class BufferPokemon:BufferPartyPokemon
	{
		public new const byte ID = 0x7D;
		public new const string NOMBRE="BufferPokemon";
		public new const string DESCRIPCION="Guarda el nombre de un pokemon en el Buffer especificado";

        public BufferPokemon(Byte buffer, Word pokemon):base(buffer,pokemon)
		{
			
 
		}
   
		public BufferPokemon(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public BufferPokemon(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe BufferPokemon(byte* ptRom, int offset)
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

	}
}
