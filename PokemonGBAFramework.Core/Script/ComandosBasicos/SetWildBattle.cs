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
		public new const int SIZE = Comando.SIZE+Word.LENGTH+1+Word.LENGTH;
		public const string NOMBRE = "SetWildBattle";
		public const string DESCRIPCION = "Prepara la batalla contra un pokemon salvaje.";
		public SetWildBattle() { }
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
		public Word Pokemon { get; set; }
		public Byte Nivel { get; set; }
		public Word Objeto { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Pokemon, Nivel, Objeto };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Nivel = ptrRom[offsetComando];
			offsetComando++;
			Objeto = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, Pokemon);
			data[3] = Nivel;
			Word.SetData(data,4, Objeto);
			
			return data;
		}
	}
}
