/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CreateSprite.
	/// </summary>
	public class CreateSprite:Comando
	{
		public const byte ID = 0xAA;
		public const int SIZE = 9;
		Byte spriteAUsar;
		Byte personajeVirtual;
		Word coordenadaX;
		Word coordenadaY;
		Byte comportamiento;
		Byte orientacion;
 
		public CreateSprite(Byte spriteAUsar, Byte personajeVirtual, Word coordenadaX, Word coordenadaY, Byte comportamiento, Byte orientacion)
		{
			SpriteAUsar = spriteAUsar;
			PersonajeVirtual = personajeVirtual;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			Comportamiento = comportamiento;
			Orientacion = orientacion;
 
		}
   
		public CreateSprite(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CreateSprite(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CreateSprite(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Crea un sprite virtual en el mapa actual.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "CreateSprite";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte SpriteAUsar {
			get{ return spriteAUsar; }
			set{ spriteAUsar = value; }
		}
		public Byte PersonajeVirtual {
			get{ return personajeVirtual; }
			set{ personajeVirtual = value; }
		}
		public Word CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Word CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
		public Byte Comportamiento {
			get{ return comportamiento; }
			set{ comportamiento = value; }
		}
		public Byte Orientacion {
			get{ return orientacion; }
			set{ orientacion = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[] {
				spriteAUsar,
				personajeVirtual,
				coordenadaX,
				coordenadaY,
				comportamiento,
				orientacion
			};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			spriteAUsar = *(ptrRom + offsetComando);
			offsetComando++;
			personajeVirtual = *(ptrRom + offsetComando);
			offsetComando++;
			coordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaY = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			comportamiento = *(ptrRom + offsetComando);
			offsetComando++;
			orientacion = *(ptrRom + offsetComando); 
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = spriteAUsar;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = personajeVirtual;
			++ptrRomPosicionado; 
			Word.SetWord(ptrRomPosicionado, CoordenadaX);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetWord(ptrRomPosicionado, CoordenadaY);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = comportamiento;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = orientacion;
		}
	}
}
