/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of MoveOffScreen.
	/// </summary>
	public class MoveOffScreen:Comando
	{
		public const byte ID = 0x64;
		public const int SIZE = 3;
		Word personaje;
 
		public MoveOffScreen(Word personaje)
		{
			Personaje = personaje;
 
		}
   
		public MoveOffScreen(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public MoveOffScreen(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe MoveOffScreen(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Changes the location of the specified sprite to a value which is exactly one tile above the top legt corner of the screen";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "MoveOffScreen";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Personaje {
			get{ return personaje; }
			set{ personaje = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personaje };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personaje = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, Personaje);
		}
	}
}
