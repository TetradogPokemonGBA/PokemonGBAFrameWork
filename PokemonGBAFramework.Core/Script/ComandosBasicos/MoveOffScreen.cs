/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of MoveOffScreen.
	/// </summary>
	public class MoveOffScreen:Comando
	{
		public const byte ID = 0x64;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "MoveOffScreen";
        public const string DESCRIPCION = "Changes the location of the specified sprite to a value which is exactly one tile above the top legt corner of the screen";
        
		public MoveOffScreen() { }
		public MoveOffScreen(Word personaje)
		{
			Personaje = personaje;
 
		}
   
		public MoveOffScreen(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public MoveOffScreen(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe MoveOffScreen(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Word Personaje { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Personaje };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Personaje = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1, Personaje);
			return data;
		}
	}
}
