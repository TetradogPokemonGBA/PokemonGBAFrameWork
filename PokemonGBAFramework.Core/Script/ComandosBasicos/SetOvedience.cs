/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetOvedience.
	/// </summary>
	public class SetOvedience:Comando
	{
		public const byte ID = 0xCD;
		public const int SIZE = 3;
		Word pokemon;
 
		public SetOvedience(Word pokemon)
		{
			Pokemon = pokemon;
 
		}
   
		public SetOvedience(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetOvedience(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetOvedience(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Hace que el pokemon seleccionado del equipo obedezca.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetOvedience";
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
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ pokemon };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			pokemon = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Pokemon);
		}
	}
}
