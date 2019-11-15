/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of GiveCoins.
	/// </summary>
	public class GiveCoins:Comando
	{
		public const byte ID = 0xB4;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "GiveCoins";
        public const string DESCRIPCION = "Da al jugador el numero especificado de fichas.";
        public GiveCoins(Word numeroDeFichasADar)
		{
			NumeroDeFichasADar = numeroDeFichasADar;
 
		}
   
		public GiveCoins(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public GiveCoins(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe GiveCoins(byte* ptRom, int offset)
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
        public Word NumeroDeFichasADar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ NumeroDeFichasADar };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			NumeroDeFichasADar = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado, NumeroDeFichasADar);
		}
	}
}
