/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ShowSprite.
	/// </summary>
	public class ShowSprite:Comando
	{
		public const byte ID = 0x55;
		public const int SIZE = 3;
		Word personajeAMostrar;
 
		public ShowSprite(Word personajeAMostrar)
		{
			PersonajeAMostrar = personajeAMostrar;
 
		}
   
		public ShowSprite(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public ShowSprite(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe ShowSprite(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Muestra un sprite previamente ocultado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowSprite";
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
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personajeAMostrar };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personajeAMostrar = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, PersonajeAMostrar);
		}
	}
}