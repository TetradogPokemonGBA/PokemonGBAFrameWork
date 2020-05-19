/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of BufferPokemon.
	/// </summary>
	public class BufferPokemon:BufferPartyPokemon
	{
		public new const byte ID = 0x7D;
		public new const string NOMBRE="BufferPokemon";
		public new const string DESCRIPCION="Guarda el nombre de un pokemon en el Buffer especificado";

		public BufferPokemon() { }
        public BufferPokemon(Byte buffer, Word pokemon):base(buffer,pokemon)
		{
			
 
		}
   
		public BufferPokemon(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public BufferPokemon(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe BufferPokemon(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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

	}
}
