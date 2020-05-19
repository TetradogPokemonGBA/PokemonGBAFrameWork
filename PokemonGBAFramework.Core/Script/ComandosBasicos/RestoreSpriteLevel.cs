/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public RestoreSpriteLevel(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public RestoreSpriteLevel(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe RestoreSpriteLevel(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			banco = ptrRom[offsetComando];
			offsetComando++;
			mapa = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Personaje);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = banco;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = mapa;
		}
	}
}
