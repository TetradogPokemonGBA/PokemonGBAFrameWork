/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetWildBattle.
	/// </summary>
	public class SetWildBattle:Comando
	{
		public const byte ID = 0xB6;
		public const int SIZE = 6;
		Word pokemon;
		Byte nivel;
		Word objeto;
 
		public SetWildBattle(Word pokemon, Byte nivel, Word objeto)
		{
			Pokemon = pokemon;
			Nivel = nivel;
			Objeto = objeto;
 
		}
   
		public SetWildBattle(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetWildBattle(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetWildBattle(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Prepara la batalla contra un pokemon salvaje.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetWildBattle";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Pokemon {
			get{ return pokemon; }
			set{ pokemon = value; }
		}
		public Byte Nivel {
			get{ return nivel; }
			set{ nivel = value; }
		}
		public Word Objeto {
			get{ return objeto; }
			set{ objeto = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ pokemon, nivel, objeto };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			nivel = ptrRom[offsetComando];
			offsetComando++;
			objeto = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Pokemon);
 
			*ptrRomPosicionado = nivel;
			++ptrRomPosicionado; 
			Word.SetData(data, , Objeto);
		}
	}
}
