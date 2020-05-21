/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SpriteLevelUp.
	/// </summary>
	public class SpriteLevelUp:Comando
	{
		public const byte ID = 0xA8;
		public const int SIZE = 6;
		Word personaje;
		Byte banco;
		Byte mapa;
		Byte unknow;
 
		public SpriteLevelUp(Word personaje, Byte banco, Byte mapa, Byte unknow)
		{
			Personaje = personaje;
			Banco = banco;
			Mapa = mapa;
			Unknow = unknow;
 
		}
   
		public SpriteLevelUp(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteLevelUp(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteLevelUp(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Hace que el sprite especificado suba un nivel en el banco y el mapa seleccionados";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SpriteLevelUp";
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
		public Byte Unknow {
			get{ return unknow; }
			set{ unknow = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personaje, banco, mapa, unknow };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			banco = ptrRom[offsetComando];
			offsetComando++;
			mapa = ptrRom[offsetComando];
			offsetComando++;
			unknow = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			Word.SetData(data, , Personaje);
 
			*ptrRomPosicionado = banco;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = mapa;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = unknow;
		}
	}
}
