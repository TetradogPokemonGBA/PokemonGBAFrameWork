/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SpriteFace.
	/// </summary>
	public class SpriteFace:Comando
	{
		public const byte ID = 0x5B;
		public const int SIZE = 4;
		Word personaje;
		Word mirandoA;
 
		public SpriteFace(Word personaje, Word mirandoA)
		{
			Personaje = personaje;
			MirandoA = mirandoA;
 
		}
   
		public SpriteFace(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SpriteFace(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SpriteFace(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Cambia donde mira el sprite";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SpriteFace";
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
		public Word MirandoA {
			get{ return mirandoA; }
			set{ mirandoA = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personaje, mirandoA };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			mirandoA = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, Personaje);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetData(ptrRomPosicionado, MirandoA);
		}
	}
}
