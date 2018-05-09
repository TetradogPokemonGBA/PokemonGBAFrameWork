/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of RestoreSpriteLevel.
	/// </summary>
	public class RestoreSpriteLevel:Comando
	{
		public const byte ID = 0xA9;
		public const int SIZE = 5;
		Word personaje;
		Byte banco;
		Byte mapa;
 
		public RestoreSpriteLevel(Word personaje, Byte banco, Byte mapa)
		{
			Personaje = personaje;
			Banco = banco;
			Mapa = mapa;
 
		}
   
		public RestoreSpriteLevel(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public RestoreSpriteLevel(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe RestoreSpriteLevel(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Restaura el nivel por defecto del personaje del mapa y banco especificado.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "RestoreSpriteLevel";
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
		public Byte Banco {
			get{ return banco; }
			set{ banco = value; }
		}
		public Byte Mapa {
			get{ return mapa; }
			set{ mapa = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personaje, banco, mapa };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			banco = *(ptrRom + offsetComando);
			offsetComando++;
			mapa = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado, Personaje);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = banco;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = mapa;
		}
	}
}
