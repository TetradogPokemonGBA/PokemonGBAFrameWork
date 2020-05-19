/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowSpritePos.
	/// </summary>
	public class ShowSpritePos:Comando
	{
		public const byte ID = 0x56;
		public const int SIZE = 5;
		Word personajeAMostrar;
		Byte coordenadaX;
		Byte coordenadaY;
 
		public ShowSpritePos(Word personajeAMostrar, Byte coordenadaX, Byte coordenadaY)
		{
			PersonajeAMostrar = personajeAMostrar;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public ShowSpritePos(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowSpritePos(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowSpritePos(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Muestra un sprite previamente ocultado. Luego aplica la posición X/Y";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowSpritePos";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word PersonajeAMostrar {
			get{ return personajeAMostrar; }
			set{ personajeAMostrar = value; }
		}
		public Byte CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Byte CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personajeAMostrar, coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personajeAMostrar = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaX = ptrRom[offsetComando];
			offsetComando++;
			coordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , PersonajeAMostrar);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = coordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = coordenadaY;
		}
	}
}
