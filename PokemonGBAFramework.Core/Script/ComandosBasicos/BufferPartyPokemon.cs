/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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

		public BufferPartyPokemon() { }
        public BufferPartyPokemon(byte buffer, Word pokemon)
		{
			Buffer = buffer;
			Pokemon = pokemon;
 
		}
   
		public BufferPartyPokemon(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public BufferPartyPokemon(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe BufferPartyPokemon(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public byte Buffer { get; set; }
        public Word Pokemon { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new object[]{ Buffer, Pokemon };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer = ptrRom[offsetComando];
			offsetComando++;
			Pokemon = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			data[1]= Buffer;
			Word.SetData(data,2, Pokemon);
			return data;
		}
	}
}
