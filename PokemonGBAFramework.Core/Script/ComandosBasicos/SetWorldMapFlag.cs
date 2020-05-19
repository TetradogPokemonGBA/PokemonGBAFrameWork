/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetWorldMapFlag.
	/// </summary>
	public class SetWorldMapFlag:Comando
	{
		public const byte ID = 0xD0;
		public const int SIZE = 3;
		Word flag;
 
		public SetWorldMapFlag(Word flag)
		{
			Flag = flag;
 
		}
   
		public SetWorldMapFlag(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetWorldMapFlag(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetWorldMapFlag(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Activa el flag que permite hacer vuelo.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetWorldMapFlag";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Flag {
			get{ return flag; }
			set{ flag = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ flag };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			flag = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Flag);
		}
	}
}
