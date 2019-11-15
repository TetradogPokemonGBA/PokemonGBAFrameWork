/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferFirstPokemon.
	/// </summary>
	public class BufferFirstPokemon:Comando
	{
		public const byte ID = 0x7E;
		public new const int SIZE = Comando.SIZE+1;
		public const string NOMBRE="BufferFirstPokemon";
		public const string DESCRIPCION="Guarda en el Buffer  especificado el nombre del primer pokemon del equipo";

        public BufferFirstPokemon(Byte buffer)
		{
			Buffer = buffer;
 
		}
   
		public BufferFirstPokemon(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public BufferFirstPokemon(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe BufferFirstPokemon(byte* ptRom, int offset)
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

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Buffer };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Buffer = ptrRom[offsetComando];

		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
            ptrRomPosicionado += base.Size;
            *ptrRomPosicionado = Buffer;

		}
	}
}
