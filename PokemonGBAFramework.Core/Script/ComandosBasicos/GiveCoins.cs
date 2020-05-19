/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public GiveCoins(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public GiveCoins(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe GiveCoins(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public Word NumeroDeFichasADar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ NumeroDeFichasADar };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			NumeroDeFichasADar = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, , NumeroDeFichasADar);
		}
	}
}
