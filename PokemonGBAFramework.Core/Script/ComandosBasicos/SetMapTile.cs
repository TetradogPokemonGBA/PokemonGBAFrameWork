/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetMapTile.
	/// </summary>
	public class SetMapTile:Comando
	{
		public const byte ID = 0xA2;
		public const int SIZE = 9;
		Word coordenadaX;
		Word coordenadaY;
		Word tile;
		Word atributoTile;
 
		public SetMapTile(Word coordenadaX, Word coordenadaY, Word tile, Word atributoTile)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			Tile = tile;
			AtributoTile = atributoTile;
 
		}
   
		public SetMapTile(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetMapTile(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetMapTile(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Pone la tile en el mapa. De alguna manera tienes que refescar esa parte.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetMapTile";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Word CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
		public Word Tile {
			get{ return tile; }
			set{ tile = value; }
		}
		public Word AtributoTile {
			get{ return atributoTile; }
			set{ atributoTile = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ coordenadaX, coordenadaY, tile, atributoTile };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			coordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaY = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			tile = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			atributoTile = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , CoordenadaX);
 
			Word.SetData(data, , CoordenadaY);
 
			Word.SetData(data, , Tile);
 
			Word.SetData(data, , AtributoTile);
		}
	}
}
