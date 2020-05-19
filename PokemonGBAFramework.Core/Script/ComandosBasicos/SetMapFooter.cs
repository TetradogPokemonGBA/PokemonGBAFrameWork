/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetMapFooter.
	/// </summary>
	public class SetMapFooter:Comando
	{
		public const byte ID = 0xA7;
		public const int SIZE = 3;
		Word footer;
 
		public SetMapFooter(Word footer)
		{
			Footer = footer;
 
		}
   
		public SetMapFooter(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetMapFooter(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetMapFooter(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Cambia el map footer actual del mapa cargando el nuevo. El mapa debe actualizarse luego para funcionar bien.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetMapFooter";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Footer {
			get{ return footer; }
			set{ footer = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ footer };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			footer = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Footer);
		}
	}
}
