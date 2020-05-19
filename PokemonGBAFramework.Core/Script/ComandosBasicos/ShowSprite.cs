/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public ShowSprite(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowSprite(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowSprite(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personajeAMostrar = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , PersonajeAMostrar);
		}
	}
}
