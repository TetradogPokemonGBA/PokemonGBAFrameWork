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
		public new const int SIZE = Comando.SIZE+Word.LENGTH + Word.LENGTH + Word.LENGTH + Word.LENGTH;
		public const string NOMBRE = "SetMapTile";
		public const string DESCRIPCION = "Pone la tile en el mapa. De alguna manera tienes que refescar esa parte.";

		public SetMapTile() { }
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
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word CoordenadaX { get; set; }
		public Word CoordenadaY { get; set; }
		public Word Tile { get; set; }
		public Word AtributoTile { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ CoordenadaX, CoordenadaY, Tile, AtributoTile };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaY = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Tile = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			AtributoTile = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];

			data[0]=IdComando;
			Word.SetData(data,1, CoordenadaX);
			Word.SetData(data,3, CoordenadaY);
			Word.SetData(data,5, Tile);
			Word.SetData(data,7, AtributoTile);

			return data;
		}
	}
}
