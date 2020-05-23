/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SpriteInvisible.
	/// </summary>
	public class SpriteInvisible:Comando
	{
		public const byte ID = 0x59;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+1+1;
		public const string NOMBRE = "SpriteInvisible";
		public const string DESCRIPCION = "Hace invisible el personaje especificado del mapa y banco";

		public SpriteInvisible() { }
		public SpriteInvisible(Word personaje, byte bank, byte mapa)
		{
			Personaje = personaje;
			Bank = bank;
			Mapa = mapa;
 
		}
   
		public SpriteInvisible(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteInvisible(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteInvisible(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public byte Bank { get; set; }
		public byte Mapa { get; set; }

		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Personaje)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Bank)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Mapa)) };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Bank = ptrRom[offsetComando++];
			Mapa = ptrRom[offsetComando++];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, Personaje);
			data[3] = Bank;
			data[4] = Mapa;

			return data;
		}
	}
}
