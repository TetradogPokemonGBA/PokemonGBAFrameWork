/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of MoveSprite.
	/// </summary>
	public class MoveSprite:Comando
	{
		public const byte ID = 0x57;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH+Word.LENGTH;
        public const string NOMBRE = "MoveSprite";
        public const string DESCRIPCION = "Mueve un sprite a una localización especifica";
        public MoveSprite(Word personajeAMover, Word coordenadaX, Word coordenadaY)
		{
			PersonajeAMover = personajeAMover;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public MoveSprite(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public MoveSprite(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe MoveSprite(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Word PersonajeAMover { get; set; }
        public Word CoordenadaX { get; set; }
        public Word CoordenadaY { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ PersonajeAMover, CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeAMover = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaY = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data, , PersonajeAMover);
 
			Word.SetData(data, , CoordenadaX);
 
			Word.SetData(data, , CoordenadaY);
		}
	}
}
