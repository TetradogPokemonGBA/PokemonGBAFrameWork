/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckObedience.
	/// </summary>
	public class CheckObedience:Comando
	{
		public const byte ID = 0xCE;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
		public const string NOMBRE="CheckObedience";
		public const string DESCRIPCION="Comprueba si el pokemon del equipo especificado obedece o no y guarda el valor en LASTRESULT.";

        public CheckObedience(Word pokemon)
		{
			Pokemon = pokemon;
 
		}
   
		public CheckObedience(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CheckObedience(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CheckObedience(byte* ptRom, int offset)
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
        public Word Pokemon { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Pokemon };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado, Pokemon); 
		}
	}
}
