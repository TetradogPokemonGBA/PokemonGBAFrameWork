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
		public new const int SIZE = 5;
		public const string NOMBRE= "RestoreSpriteLevel";
		public const string DESCRIPCION= "Restaura el nivel por defecto del personaje del mapa y banco especificado.";

		public RestoreSpriteLevel() { }
		public RestoreSpriteLevel(Word personaje, Byte banco, Byte mapa)
		{
			Personaje = personaje;
			Banco = banco;
			Mapa = mapa;
 
		}
   
		public RestoreSpriteLevel(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public RestoreSpriteLevel(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe RestoreSpriteLevel(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Word Personaje { get; set; }
		public Byte Banco { get; set; }
		public Byte Mapa { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Personaje, Banco, Mapa };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Banco = ptrRom[offsetComando];
			offsetComando++;
			Mapa = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			Word.SetData(data,1, Personaje);
			data[3]= Banco;
			data[4] = Mapa;
			return data;

		}
	}
}
