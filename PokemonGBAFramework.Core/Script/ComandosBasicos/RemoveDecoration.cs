/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of RemoveDecoration.
	/// </summary>
	public class RemoveDecoration:Comando
	{
		public const byte ID = 0x4C;
		public const int SIZE = 3;
		Word decoracion;
 
		public RemoveDecoration(Word decoracion)
		{
			Decoracion = decoracion;
 
		}
   
		public RemoveDecoration(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public RemoveDecoration(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe RemoveDecoration(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Quita del pc del player la cantidad del objetodo decorativo";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "RemoveDecoration";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Decoracion {
			get{ return decoracion; }
			set{ decoracion = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ decoracion };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			decoracion = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Decoracion);
		}
	}
}
