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
		public new const int SIZE = Comando.SIZE+Word.LENGTH+1+1+1;

		public const string NOMBRE = "SpriteLevelUp";
		public const string DESCRIPCION = "Hace que el sprite especificado suba un nivel en el banco y el mapa seleccionados";

		public SpriteLevelUp() { }
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
		public Word Personaje { get; set; }
		public Byte Banco { get; set; }
		public Byte Mapa { get; set; }
		public Byte Unknow { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Personaje, Banco, Mapa, Unknow };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Banco = ptrRom[offsetComando];
			offsetComando++;
			Mapa = ptrRom[offsetComando];
			offsetComando++;
			Unknow = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, Personaje);
			data[3] = Banco;
			data[4] = Mapa;
			data[5]= Unknow;

			return data;
		}
	}
}
