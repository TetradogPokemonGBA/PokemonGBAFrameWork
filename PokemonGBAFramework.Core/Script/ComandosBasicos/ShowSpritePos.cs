/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using Gabriel.Cat.S.Extension;
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowSpritePos.
	/// </summary>
	public class ShowSpritePos:ShowSprite
	{
		public new const byte ID = 0x56;
		public new const int SIZE =ShowSprite.SIZE+1+1;
		public new const string NOMBRE = "ShowSpritePos";
		public new const string DESCRIPCION = "Muestra un sprite previamente ocultado. Luego aplica la posición X/Y";

		public ShowSpritePos() { }
		public ShowSpritePos(Word personajeAMostrar, Byte coordenadaX, Byte coordenadaY):base(personajeAMostrar)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public ShowSpritePos(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowSpritePos(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowSpritePos(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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

		public Byte CoordenadaX { get; set; }
		public Byte CoordenadaY { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ PersonajeAMostrar, CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(scriptManager, ptrRom, offsetComando);
			offsetComando += base.ParamsSize;
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			return base.GetBytesTemp().AddArray(new byte[] { CoordenadaX, CoordenadaY });
		}
	}
}
