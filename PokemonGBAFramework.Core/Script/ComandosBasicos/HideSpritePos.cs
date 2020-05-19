/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of HideSpritePos.
	/// </summary>
	public class HideSpritePos:Comando
	{
		public const byte ID = 0x54;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+1+1;
        public const string NOMBRE = "HideSpritePos";
        public const string DESCRIPCION = "Oculta un sprite y luego aplica la posición X/Y";
        public HideSpritePos(Word personajeAOcultar, Byte coordenadaX, Byte coordenadaY)
		{
			PersonajeAOcultar = personajeAOcultar;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public HideSpritePos(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public HideSpritePos(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe HideSpritePos(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public Word PersonajeAOcultar { get; set; }
        public Byte CoordenadaX { get; set; }
        public Byte CoordenadaY { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ PersonajeAOcultar, CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeAOcultar = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
			offsetComando++;
 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, , PersonajeAOcultar);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = CoordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = CoordenadaY;
		}
	}
}
