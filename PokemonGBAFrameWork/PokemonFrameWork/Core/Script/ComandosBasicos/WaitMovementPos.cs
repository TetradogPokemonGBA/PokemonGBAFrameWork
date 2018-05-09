/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of WaitMovementPos.
	/// </summary>
	public class WaitMovementPos:Comando
	{
		public const byte ID = 0x52;
		public const int SIZE = 5;
		Word personajeAEsperar;
		Byte coordenadaX;
		Byte coordenadaY;
 
		public WaitMovementPos(Word personajeAEsperar, Byte coordenadaX, Byte coordenadaY)
		{
			PersonajeAEsperar = personajeAEsperar;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public WaitMovementPos(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public WaitMovementPos(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe WaitMovementPos(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Espera a que acabe el ApplyMovement del personaje especificado y luego pone las coordenadas X/Y";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WaitMovementPos";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word PersonajeAEsperar {
			get{ return personajeAEsperar; }
			set{ personajeAEsperar = value; }
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
			return new Object[]{ personajeAEsperar, coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personajeAEsperar = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaX = *(ptrRom + offsetComando);
			offsetComando++;
			coordenadaY = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, PersonajeAEsperar);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = coordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = coordenadaY;
		}
	}
}
