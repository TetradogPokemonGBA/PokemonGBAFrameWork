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

		public CheckObedience() { }
        public CheckObedience(Word pokemon)
		{
			Pokemon = pokemon;
 
		}
   
		public CheckObedience(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CheckObedience(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CheckObedience(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Word Pokemon { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Pokemon };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1, Pokemon);
			return data;
		}
	}
}
