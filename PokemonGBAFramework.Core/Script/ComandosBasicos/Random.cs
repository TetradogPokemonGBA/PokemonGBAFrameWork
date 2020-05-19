/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Random.
	/// </summary>
	public class Random:Comando
	{
		public const byte ID = 0x8F;
		public new const int SIZE = 3;
        public const string NOMBRE = "Random";
        public const string DESCRIPCION = "Genera un numero random entre 0 y NumeroFin";

        public Random(Word numeroFin)
		{
			NumeroFin = numeroFin;
 
		}
   
		public Random(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Random(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Random(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public Word NumeroFin { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ NumeroFin };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			NumeroFin = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , NumeroFin);
		}
	}
}
