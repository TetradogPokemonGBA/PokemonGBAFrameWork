/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of HideSprite.
	/// </summary>
	public class HideSprite:Comando
	{
		public const byte ID = 0x53;
		public const int SIZE = 3;
		Word personajeAOcultar;
 
		public HideSprite(Word personajeAOcultar)
		{
			PersonajeAOcultar = personajeAOcultar;
 
		}
   
		public HideSprite(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public HideSprite(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe HideSprite(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Oculta un Sprite";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "HideSprite";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word PersonajeAOcultar {
			get{ return personajeAOcultar; }
			set{ personajeAOcultar = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personajeAOcultar };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personajeAOcultar = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, PersonajeAOcultar);
		}
	}
}
